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
        private string _windowsVolume;
        private string _sigCheckPath;
        private List<DriveMapping> _driveMappings;
        private bool _hasErrors = false;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="driveMappings"></param>
        /// <param name="registryPath"></param>
        public void Start(Configuration configuration,
                          List<DriveMapping> driveMappings, 
                          string registryPath)
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
                Registry.Registry registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.Open(@"Select");
                if (regKey == null)
                {
                    continue;
                }

                RegistryValue regValueCcs = regKey.Value("Current");
                if (regValueCcs == null)
                {
                    continue;
                }

                RegistryKey regKeyServices = registry.Open(@"ControlSet00" + regValueCcs.Value + "\\Services");
                if (regKeyServices == null)
                {
                    continue;
                }

                foreach (RegistryKey key in regKeyServices.SubKeys())
                {
                    try
                    {
                        DateTime modified;
                        string imagePath = string.Empty;
                        string serviceDll = string.Empty;
                        if (key.Value("ImagePath") == null)
                        {
                            continue;
                        }

                        imagePath = key.Value("ImagePath").Value.ToString();
                        modified = key.Timestamp;

                        if (key.SubKey("Parameters") != null)
                        {
                            if (key.SubKey("Parameters").Value("ServiceDll") != null)
                            {
                                serviceDll = key.SubKey("Parameters").Value("ServiceDll").Value.ToString();
                                modified = key.SubKey("Parameters").Timestamp;
                            }
                        }

                        string serviceType = string.Empty;
                        if (key.Value("Type") != null)
                        {
                            serviceType = key.Value("Type").Value.ToString();
                        }

                        string description = string.Empty;
                        if (key.Value("Description") != null)
                        {
                            description = key.Value("Description").Value.ToString();
                        }

                        string displayName = string.Empty;
                        if (key.Value("DisplayName") != null)
                        {
                            displayName = key.Value("DisplayName").Value.ToString();
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
                                         key.Path,
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
                                         key.Path,
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
                Registry.Registry registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = OpenKey(registry, config.Path);
                if (regKey == null)
                {
                    continue;
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
        private void EnumerateValues(RegistryKey regKey, string path, string type, string info, string sourceFile)
        {
            DateTime modified = regKey.Timestamp;
            foreach (RegistryValue regValue in regKey.Values())
            {
                ProcessEntry(regValue.Value.ToString(), path, type, info, sourceFile, modified);
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
                Registry.Registry registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.Open(@"Microsoft\Windows NT\CurrentVersion\ProfileList");
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey key in regKey.SubKeys())
                {
                    if (key.Value("ProfileImagePath") == null)
                    {
                        continue;
                    }

                    string profilePath = key.Value("ProfileImagePath").Value.ToString();
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
                Registry.Registry registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = OpenKey(registry, config.Path);
                if (regKey == null)
                {
                    continue;
                }

                if (regKey.Value("Appinit_Dlls") == null)
                {
                    continue;
                }

                if (regKey.Value("Appinit_Dlls").Value.ToString().Length == 0)
                {
                    continue;
                }

                ParseAppInitDll(config.Path, regKey.Value("Appinit_Dlls").Value.ToString(), file, regKey.Timestamp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        private void ParseAppInitDll(string path, string data, string sourceFile, DateTime modified)
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
                Registry.Registry registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.Open(config.Path);
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey subKey in regKey.SubKeys())
                {
                    if (subKey.Value("StubPath") == null)
                    {
                        continue;
                    }

                    ProcessEntry(subKey.Value("StubPath").Value.ToString(), 
                                 @"HLKM\" + config.Hive.GetEnumDescription() + @"\" + config.Path + @"\" + subKey.Name,
                                 config.Type,
                                 @"HLKM\Software\Microsoft\Active Setup\Installed Components\" + subKey.Name,
                                 file,
                                 subKey.Timestamp);
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
                Registry.Registry registry = OpenRegistry(file);
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

                    foreach (RegistryKey subKey in regKey.SubKeys())
                    {
                        if (subKey.Value("(default)") == null)
                        {
                            continue;
                        }

                        string guid = subKey.Value("(default)").Value.ToString();
                        ProcessClsid(registry, 
                                     pathPrefix + "\\" + key,
                                     subKey.Name, 
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
                Registry.Registry registry = OpenRegistry(file);
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
        private void ProcessClsid(Registry.Registry registry, 
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

            if (regKey.Value("(default)") == null)
            {
                return;
            }

            ProcessEntry(regKey.Value("(default)").Value.ToString(), path, type, info, sourceFile, regKey.Timestamp);
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
                Registry.Registry registry = OpenRegistry(file);
                if (registry == null)
                {
                    continue;
                }

                RegistryKey regKey = registry.Open(config.Path);
                if (regKey == null)
                {
                    continue;
                }

                foreach (RegistryKey tempKey in regKey.SubKeys())
                {
                    ProcessClsid(registry, config.Path, "BHO", tempKey.Name, tempKey.Name, file);
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
                                  DateTime regModified)
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
                                  DateTime regModified)
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

                        string output = Misc.ShellProcessWithOutput(_sigCheckPath, Global.SIGCHECK_FLAGS + "\"" + autoRunEntry.FilePath + "\"");
                        autoRunEntry = Helper.ParseSigCheckOutput(autoRunEntry, output);
                        autoRunEntry.Md5 = Security.GenerateMd5HashStream(autoRunEntry.FilePath);

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
        private Registry.Registry OpenRegistry(string file)
        {
            try
            {
                return new Registry.Registry(file);
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Registry.RegistryKey OpenKey(Registry.Registry registry,
                                             string key)
        {
            try
            {
                return registry.Open(key);
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
