using ProcessPrivileges;
using Registry;
using Shellify;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using woanware;
using Registry.Abstractions;
using wincatalogdotnet;

namespace autorunner
{
    public class Importer
    {
        #region Delegates
        public delegate void CompleteEvent(List<AutoRunEntry> entries);
        public delegate void MessageEvent(string message);
        public delegate void EntryFoundEvent(AutoRunEntry autoRunEntry);
        #endregion

        #region Events
        public event CompleteEvent Complete;
        public event MessageEvent Message;
        public event MessageEvent Error;
        public event EntryFoundEvent EntryFound;
        #endregion

        #region Member Variables
        public bool IsRunning { get; private set; }
        private Configuration _configuration;
        private List<AutoRunEntry> _entries;
        private string _registryPath;
        private string _catalogPath;
        private string _windowsVolume;
        private string _sigCheckPath;
        private List<DriveMapping> _driveMappings;
        private bool _hasErrors = false;
        private Dictionary<string, bool> hashes = new Dictionary<string, bool>();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="driveMappings"></param>
        /// <param name="registryPath"></param>
        /// <param name="catalogPath"></param>
        public void Start(Configuration configuration,
                          List<DriveMapping> driveMappings, 
                          string registryPath,
                          string catalogPath)
        {
            if (IsRunning == true)
            {
                OnError("The importer is already running");
                return;
            }

            IsRunning = true;

            _configuration = configuration;
            _driveMappings = driveMappings;
            _registryPath = registryPath;
            _catalogPath = catalogPath;

            // Get the windows drive mapping
            var windowsDrive = (from d in _driveMappings where d.IsWindowsDrive == true select d).SingleOrDefault();
            _windowsVolume = System.IO.Path.GetPathRoot(windowsDrive.MappedDrive);

            _sigCheckPath = System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Tools", "sigcheck.exe");

            Thread thread = new Thread(new ThreadStart(Process));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Process()
        {
            LoadCatalogHashes(); 

            IntPtr ptr = new IntPtr();
            Native.Wow64DisableWow64FsRedirection(ref ptr);

            _entries = new List<AutoRunEntry>();

            foreach (Config config in _configuration.Data)
            {
                switch (config.EntryType)
                {
                    case Global.AutoRunType.Driver:
                        EnumerateServicesAndDrivers(config, false);
                        break;
                    case Global.AutoRunType.FolderDefault:
                        EnumerateDefaultStartUpFiles(config);
                        break;
                    case Global.AutoRunType.FolderUsers:
                        EnumerateUserStartUpFiles(config);
                        break;
                    case Global.AutoRunType.RegistryStubValue:
                        EnumerateBinaryFromStubPath(config);
                        break;
                    case Global.AutoRunType.RegistryValue:
                        EnumerateBinaryFromValue(config);
                        break;
                    case Global.AutoRunType.Service:
                        EnumerateServicesAndDrivers(config, true);
                        break;
                    case Global.AutoRunType.AppInit:
                        EnumerateAppInitDlls(config);
                        break;
                    case Global.AutoRunType.Bho:
                        EnumerateBho(config);
                        break;
                }
            }

            EnumerateDllHijackingBinaries();

            Native.Wow64RevertWow64FsRedirection(ptr);

            if (_hasErrors == true)
            {
                OnError("Errors occurred during the processing. Check the 'Errors.txt' file in the following directory: " + Environment.NewLine + Environment.NewLine + Misc.GetUserDataDirectory());
            }

            OnComplete();
            IsRunning = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadCatalogHashes()
        {
            string[] files = System.IO.Directory.GetFiles(_catalogPath, "*.cat", SearchOption.AllDirectories);

            hashes = new Dictionary<string, bool>();

            foreach (var file in files)
            {
                try
                {
                    int catVer;
                    var temp = WinCatalog.GetHashesFromCatalog(file, out catVer);
                    foreach (string hash in temp)
                    {
                        hashes[hash] = true;
                    }
                }
                catch (Exception ex)
                {
                    _hasErrors = true;
                    Helper.WriteErrorToLog(ex.Message, string.Empty, string.Empty, file);
                }
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void EnumerateDllHijackingBinaries()
        {
            foreach (DriveMapping dm in _driveMappings)
            {
               // string output = Misc.ShellProcessWithOutput(_sigCheckPath, dm.MappedDrive Global.SIGCHECK_FLAGS + "\"" + autoRunEntry.FilePath + "\"");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="services"></param>
        private void EnumerateServicesAndDrivers(Config config, bool services)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath, 
                                                                       config.Hive.GetEnumDescription(), 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.GetKey(@"Select");
                if (regKey == null)
                {
                    continue;
                }

                KeyValue regValueCcs = regKey.Values.Where(v => v.ValueName.ToLower() == "current").SingleOrDefault();
                if (regValueCcs == null)
                {
                    continue;
                }

                regKey = registry.GetKey(@"ControlSet00" + regValueCcs.ValueData + "\\Services");
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey key in regKey.SubKeys)
                {
                    try
                    {
                        RegistryKey subKey = registry.GetKey(key.KeyPath);

                        DateTimeOffset? modified;
                        string imagePath = string.Empty;
                        string serviceDll = string.Empty;
                        if (subKey.Values.Where(v => v.ValueName.ToLower() == "imagepath").SingleOrDefault() == null)
                        {
                            continue;
                        }

                        if (subKey.KeyPath == @"CsiTool-CreateHive-{00000000-0000-0000-0000-000000000000}\ControlSet001\Services\AppReadiness")
                        {

                        }

                        imagePath = subKey.Values.Where(v => v.ValueName.ToLower() == "imagepath").SingleOrDefault().ValueData;
                        modified = subKey.LastWriteTime;

                        if (subKey.SubKeys.Where(v => v.KeyName.ToLower() == "parameters").SingleOrDefault() != null)
                        {
                            RegistryKey subKeyParameters = subKey.SubKeys.Where(v => v.KeyName.ToLower() == "parameters").SingleOrDefault();
                            subKeyParameters = registry.GetKey(subKeyParameters.KeyPath);

                            if (subKeyParameters.Values.Where(v => v.ValueName.ToLower() == "servicedll").SingleOrDefault() != null)
                            {
                                serviceDll = subKeyParameters.Values.Where(v => v.ValueName.ToLower() == "servicedll").SingleOrDefault().ValueData;
                                modified = subKeyParameters.LastWriteTime;
                            }
                        }

                        string serviceType = string.Empty;
                        if (subKey.Values.Where(v => v.ValueName.ToLower() == "type").SingleOrDefault() != null)
                        {
                            serviceType = subKey.Values.Where(v => v.ValueName.ToLower() == "type").SingleOrDefault().ValueData;
                        }

                        string description = string.Empty;
                        if (subKey.Values.Where(v => v.ValueName.ToLower() == "description").SingleOrDefault() != null)
                        {
                            description = subKey.Values.Where(v => v.ValueName.ToLower() == "description").SingleOrDefault().ValueData;
                        }

                        string displayName = string.Empty;
                        if (subKey.Values.Where(v => v.ValueName.ToLower() == "displayName").SingleOrDefault() != null)
                        {
                            displayName = subKey.Values.Where(v => v.ValueName.ToLower() == "displayName").SingleOrDefault().ValueData;
                        }

                        switch (serviceType)
                        {
                            case "1":
                                serviceType = "Kernel Device Driver";
                                break;
                            case "2":
                                serviceType = "File System Driver";
                                break;
                            case "16":
                                serviceType = "Service";
                                break;
                            case "32":
                                serviceType = "Shared Service";
                                break;
                            default:
                                break;
                        }

                        if (serviceDll.Length > 0)
                        {
                            ProcessEntry(serviceDll,
                                         subKey.KeyPath,
                                         config.Type,
                                         string.Empty,
                                         file,
                                         displayName,
                                         description,
                                         modified);
                        }
                        else
                        {
                            ProcessEntry(imagePath,
                                         subKey.KeyPath,
                                         config.Type,
                                         string.Empty,
                                         file,
                                         displayName,
                                         description,
                                         modified);
                        }
                    }
                    catch (Exception) { continue; }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EnumerateBinaryFromValue(Config config)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath, 
                                                                       config.Hive.GetEnumDescription(), 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = null;

                if (config.Path.IndexOf("currentcontrolset", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    RegistryKey regKeyTemp = registry.GetKey(@"Select");
                    if (regKeyTemp == null)
                    {
                        return;
                    }

                    KeyValue regValueCcs = regKeyTemp.Values.Where(v => v.ValueName.ToLower() == "current").SingleOrDefault();
                    if (regValueCcs == null)
                    {
                        return;
                    }

                    regKey = registry.GetKey(@"ControlSet00" + regValueCcs.ValueData);
                    if (regKey == null)
                    {
                        return;
                    }
                }
                else
                {
                    regKey = OpenKey(registry, config.Path);
                    if (regKey == null)
                    {
                        continue;
                    }
                }                

                string prefix = string.Empty;
                switch (config.Hive)
                {
                    case Global.Hive.Ntuser:
                        prefix = "HKCU";
                        break;
                    case Global.Hive.Software:
                        prefix = @"HKLM\Software";
                        break;
                    case Global.Hive.System:
                        prefix = @"HKLM\System";
                        break;
                    case Global.Hive.Usrclass:
                        prefix = "HKCU";
                        break;
                    default:
                        break;
                }

                EnumerateValues(regKey, prefix + "\\" + config.Path, config.Type, string.Empty, file);
            }
        }

        /// <summary>
        /// Generic registry helper to enumerate all registry values
        /// </summary>
        /// <param name="regKey"></param>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <param name="sourceFile"></param>
        private void EnumerateValues(Registry.Abstractions.RegistryKey regKey, string path, string type, string info, string sourceFile)
        {
            DateTimeOffset? modified = regKey.LastWriteTime;
            foreach (Registry.Abstractions.KeyValue regValue in regKey.Values)
            {
                ProcessEntry(regValue.ValueData.ToString(), path, type, info, sourceFile, modified);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EnumerateUserStartUpFiles(Config config)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath, 
                                                                       config.Hive.GetEnumDescription(), 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.GetKey(@"Microsoft\Windows NT\CurrentVersion\ProfileList");
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey key in regKey.SubKeys)
                {
                    RegistryKey subKey = registry.GetKey(key.KeyPath);

                    if (subKey.Values.Where(v => v.ValueName.ToLower() == "profileimagepath").SingleOrDefault() == null)
                    {
                        continue;
                    }

                    string profilePath = subKey.Values.Where(v => v.ValueName.ToLower() == "profileimagepath").SingleOrDefault().ValueData;
                    string startupPath = System.IO.Path.Combine(profilePath, config.Path);
                    startupPath = Helper.NormalisePath(_driveMappings, startupPath);
                    if (System.IO.Directory.Exists(startupPath) == false)
                    {
                        continue;
                    }

                    try
                    {
                        foreach (string startupFile in System.IO.Directory.EnumerateFiles(startupPath, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.Path.GetFileName(startupFile).ToLower() == "desktop.ini")
                            {
                                continue;
                            }

                            ProcessEntry(startupFile, startupPath, config.Type, profilePath, file, DateTime.MinValue);
                        }
                    }
                    catch (Exception ex) 
                    {
                        _hasErrors = true;
                        Helper.WriteErrorToLog(ex.Message, string.Empty, string.Empty, config.Path);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EnumerateDefaultStartUpFiles(Config config)
        {
            string startupPath = System.IO.Path.Combine(_windowsVolume, config.Path);
            if (System.IO.Directory.Exists(startupPath) == false)
            {
                return;
            }

            try
            {
                foreach (string startupFile in System.IO.Directory.EnumerateFiles(startupPath, "*.*", SearchOption.AllDirectories))
                {
                    ProcessEntry(startupFile, startupPath, "User Start Up", string.Empty, startupFile, DateTime.MinValue);
                }
            }
            catch (Exception ex)
            {
                _hasErrors = true;
                Helper.WriteErrorToLog(ex.Message, string.Empty, string.Empty, config.Path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EnumerateAppInitDlls(Config config)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath, 
                                                                       config.Hive.GetEnumDescription(), 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = OpenKey(registry, config.Path);
                if (regKey == null)
                {
                    continue;
                }

                if (regKey.Values.Where(v => v.ValueName.ToLower() == "appinit_dlls").SingleOrDefault() == null)
                {
                    continue;
                }

                if (regKey.Values.Where(v => v.ValueName.ToLower() == "appinit_dlls").SingleOrDefault().ValueData.Length == 0)
                {
                    continue;
                }

                ParseAppInitDll(config.Path, regKey.Values.Where(v => v.ValueName.ToLower() == "appinit_dlls").SingleOrDefault().ValueData, file, regKey.LastWriteTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        private void ParseAppInitDll(string path, string data, string sourceFile, DateTimeOffset? modified)
        {
            string[] parts = data.Split(' ');
            foreach (string part in parts)
            {
                StringBuilder longPath = new StringBuilder(255);
                Native.GetLongPathName(part, longPath, longPath.Capacity);
                ProcessEntry(longPath.ToString(), path, "AppInit", string.Empty, sourceFile, modified);
            }

            parts = data.Split(',');
            foreach (string part in parts)
            {
                StringBuilder longPath = new StringBuilder(255);
                Native.GetLongPathName(part, longPath, longPath.Capacity);
                ProcessEntry(longPath.ToString(), path, "AppInit", string.Empty, sourceFile, modified);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EnumerateBinaryFromStubPath(Config config)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath,
                                                                       config.Hive.GetEnumDescription(), 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.GetKey(config.Path);
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey subKey in regKey.SubKeys)
                {
                    RegistryKey childKey = registry.GetKey(subKey.KeyPath);
                    if (childKey.Values.Where(v => v.ValueName.ToLower() == "stubpath").SingleOrDefault() == null)
                    {
                        continue;
                    }

                    ProcessEntry(childKey.Values.Where(v => v.ValueName.ToLower() == "stubpath").SingleOrDefault().ValueData,
                                 @"HLKM\" + config.Hive.GetEnumDescription() + @"\" + config.Path + @"\" + childKey.KeyName,
                                 config.Type,
                                 @"HLKM\Software\Microsoft\Active Setup\Installed Components\" + childKey.KeyName,
                                 file,
                                 childKey.LastWriteTime);
                }
            }
        }

        /// <summary>
        /// NOT USED
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="pathPrefix"></param>
        /// <param name="files"></param>
        /// <param name="type"></param>
        private void EnumerateShellEx(List<string> keys,
                                      string pathPrefix,
                                      string files,
                                      string type)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath, 
                                                                       files, 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                foreach (string key in keys)
                {
                    RegistryKey regKey = OpenKey(registry, key);
                    if (regKey == null)
                    {
                        continue;
                    }

                    foreach (RegistryKey subKey in regKey.SubKeys)
                    {
                        if (subKey.Values.Where(v => v.ValueName.ToLower() == "default").SingleOrDefault() == null)
                        {
                            continue;
                        }

                        string guid = subKey.Values.Where(v => v.ValueName.ToLower() == "default").SingleOrDefault().ValueData;
                        ProcessClsid(registry, 
                                     pathPrefix + "\\" + key,
                                     subKey.KeyName, 
                                     "ShellEx",
                                     guid, 
                                     file);
                    }
                }
            }
        }

        /// <summary>
        /// NOT USED
        /// </summary>
        private void EnumerateShellExHooks()
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath, 
                                                                       "software", 
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                //foreach (string key in _keysGuidValue)
                //{
                //    RegistryKey regKey = registry.Open(key);
                //    if (regKey == null)
                //    {
                //        continue;
                //    }

                //    foreach (RegistryValue regValue in regKey.Values())
                //    {
                //        ProcessClsid(registry, "HKLM\\Software\\" + key, key, "ShellExecuteHooks", regValue.Value.ToString());
                //    }
                //}
            }
        }

        /// <summary>
        /// Lookup the Classes\CLSID key, then the  "InprocServer32" key of the GUID\InprocServer32, then the "(default)" value, normalise path, then check
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <param name="guid"></param>
        /// <param name="sourceFile"></param>
        private void ProcessClsid(RegistryHiveOnDemand registry, 
                                  string path, 
                                  string type, 
                                  string info, 
                                  string guid, 
                                  string sourceFile)
        {
            RegistryKey regKey = OpenKey(registry, @"Classes\CLSID\" + guid + @"\InprocServer32");
            if (regKey == null)
            {
                regKey = OpenKey(registry, @"Classes\Wow6432Node\CLSID\" + guid + @"\InprocServer32");
                if (regKey == null)
                {
                    return;
                }
            }

            if (regKey.Values.Where(v => v.ValueName.ToLower() == "(default)").SingleOrDefault() == null)
            {
                return;
            }

            ProcessEntry(regKey.Values.Where(v => v.ValueName.ToLower() == "(default)").SingleOrDefault().ValueData, path, type, info, sourceFile, regKey.LastWriteTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EnumerateBho(Config config)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(_registryPath,
                                                                       config.Hive.GetEnumDescription(),
                                                                       SearchOption.AllDirectories))
            {
                RegistryHiveOnDemand registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.GetKey(config.Path);
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey tempKey in regKey.SubKeys)
                {
                    ProcessClsid(registry, config.Path, "BHO", tempKey.KeyName, tempKey.KeyName, file);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <param name="sourceFile">The registry file the entry was identified from</param>
        /// <param name="regModified"></param>
        private void ProcessEntry(string filePath,
                                  string path,
                                  string type,
                                  string info,
                                  string sourceFile,
                                  DateTimeOffset? regModified)
        {
            ProcessEntry(filePath, path, type, info, sourceFile, string.Empty, string.Empty, regModified);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <param name="sourceFile">The registry file the entry was identified from</param>
        /// <param name="regModified"></param>
        private void ProcessEntry(string filePath, 
                                  string path, 
                                  string type, 
                                  string info,
                                  string sourceFile, 
                                  string serviceDisplayName, 
                                  string serviceDescription,
                                  DateTimeOffset? regModified)
        {
            try
            {
                filePath = Text.ReplaceNulls(filePath);
                AutoRunEntry autoRunEntry = new AutoRunEntry();
                autoRunEntry.Type = type;
                autoRunEntry.Info = info;
                autoRunEntry.Path = path;
                autoRunEntry.ServiceDisplayName = serviceDisplayName;
                autoRunEntry.ServiceDescription = serviceDescription;
                autoRunEntry.SourceFile = sourceFile;
                autoRunEntry = Helper.GetFilePathWithNoParameters(_driveMappings, autoRunEntry, filePath);
                if (autoRunEntry == null)
                {
                    return;
                }

                if (autoRunEntry.FilePath.Length == 0)
                {
                    return;
                }

                autoRunEntry.FileName = System.IO.Path.GetFileName(autoRunEntry.FilePath.Replace("\"", string.Empty));

                if (System.IO.Path.GetExtension(autoRunEntry.FileName) == ".lnk")
                {
                    try
                    {
                        ShellLinkFile shellLinkFile = ShellLinkFile.Load(autoRunEntry.FilePath);

                        autoRunEntry = new AutoRunEntry();
                        autoRunEntry.Type = type;
                        autoRunEntry.Info = info;
                        autoRunEntry.Info = filePath;
                        autoRunEntry = Helper.GetFilePathWithNoParameters(_driveMappings, autoRunEntry, System.IO.Path.Combine(shellLinkFile.LinkInfo.LocalBasePath, shellLinkFile.LinkInfo.CommonPathSuffix) + " " + shellLinkFile.Arguments);
                        autoRunEntry.Path = System.IO.Path.Combine(shellLinkFile.LinkInfo.LocalBasePath, shellLinkFile.LinkInfo.CommonPathSuffix) + " " + shellLinkFile.Arguments;
                        
                        autoRunEntry.FileName = System.IO.Path.GetFileName(autoRunEntry.FilePath.Replace("\"", string.Empty));
                    }
                    catch (Exception ex)
                    {
                        _hasErrors = true;
                        Helper.WriteErrorToLog(ex.Message, filePath, autoRunEntry.FilePath, path);
                    }
                }

                if (autoRunEntry.FilePath.Length == 0)
                {
                    Helper.WriteErrorToLog("FilePath is zero length", filePath, autoRunEntry.FilePath, path);
                    return;
                }

                Console.WriteLine(autoRunEntry.FilePath);

                _entries.Add(autoRunEntry);

                try
                {
                    using (new PrivilegeEnabler(System.Diagnostics.Process.GetCurrentProcess(), Privilege.TakeOwnership))
                    {
                        FileInfo fileInfo = new FileInfo(autoRunEntry.FilePath);
                        FileSecurity fileSecurity = fileInfo.GetAccessControl();

                        fileSecurity.SetOwner(WindowsIdentity.GetCurrent().User);
                        File.SetAccessControl(autoRunEntry.FilePath, fileSecurity);

                        if (File.Exists(autoRunEntry.FilePath) == false)
                        {
                            autoRunEntry.Error = "Does Not Exist";
                            OnEntryFound(autoRunEntry);
                            return;
                        }

                        autoRunEntry.Exists = true;
                        autoRunEntry.FileSystemAccessed = fileInfo.LastAccessTime;
                        autoRunEntry.FileSystemCreated = fileInfo.CreationTime;
                        autoRunEntry.FileSystemModified = fileInfo.LastWriteTime;
                        autoRunEntry.RegistryModified = regModified;

                        //string output = Misc.ShellProcessWithOutput(_sigCheckPath, Global.SIGCHECK_FLAGS + "\"" + autoRunEntry.FilePath + "\"");
                        //autoRunEntry = Helper.ParseSigCheckOutput(autoRunEntry, output);
                        autoRunEntry = Helper.GetFileInformation(hashes, _sigCheckPath, autoRunEntry);
                        autoRunEntry.Md5 = Security.GenerateMd5HashStream(autoRunEntry.FilePath);
                        autoRunEntry.Sha256 = Helper.GetFileSha256Hash(autoRunEntry.FilePath);

                        OnEntryFound(autoRunEntry);
                    }
                }
                catch (Exception ex)
                {
                    Helper.WriteErrorToLog(ex.Message, filePath, autoRunEntry.FilePath, path);
                    autoRunEntry.Error = "Error: " + ex.Message;
                    OnEntryFound(autoRunEntry);
                }
            }
            catch (Exception ex)
            {
                _hasErrors = true;
                Helper.WriteErrorToLog(ex.Message, filePath, filePath, path);
            }
        }

        #region Registry Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private Registry.RegistryHiveOnDemand OpenRegistry(string file)
        {
            try
            {
                return new Registry.RegistryHiveOnDemand(file);
            }
            catch (Exception ex) 
            {
                return null; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Registry.Abstractions.RegistryKey OpenKey(Registry.RegistryHiveOnDemand registry,
                                                          string key)
        {
            try
            {
                return registry.GetKey(key);
            }
            catch (Exception ex) 
            { 
                return null;
            }
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            var handler = Complete;
            if (handler != null)
            {
                handler(_entries);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnEntryFound(AutoRunEntry autoRunEntry)
        {
            var handler = EntryFound;
            if (handler != null)
            {
                handler(autoRunEntry);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnMessage(string message)
        {
            var handler = Message;
            if (handler != null)
            {
                handler(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnError(string message)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(message);
            }
        }
        #endregion
    }
}
