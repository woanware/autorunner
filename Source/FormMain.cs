using BrightIdeasSoftware;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using woanware;
using System.Net;
using Ionic.Zip;

namespace autorunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMain : Form
    {
        #region Member Variables
        private Configuration _configuration;
        private Importer _importer;
        private HourGlass _hourGlass;
        private Settings _settings;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            _importer = new Importer();
            _importer.EntryFound += OnImporter_EntryFound;
            _importer.Complete += OnImporter_Complete;

            // This is a delegate that allows us to set a custom backcolour 
            // depending on the entries properties e.g. exists on disk/signed
            listEntries.RowFormatter = delegate(OLVListItem olvi) 
            { 
                AutoRunEntry autoRunEntry = (AutoRunEntry)olvi.RowObject;
                if (autoRunEntry.Exists == false)
                {
                    olvi.BackColor = Color.FromArgb(255, 246, 127); // Yellow
                }
                else
                {
                    if (autoRunEntry.Verified.ToLower() != "signed")
                    {
                        olvi.BackColor = Color.FromArgb(255, 94, 76); // Red
                    }
                }
            };

            SysImageListHelper helper = new SysImageListHelper(this.listEntries);
            this.olvcType.ImageGetter = delegate(object x)
            {
                AutoRunEntry autoRunEntry = (AutoRunEntry)x;
                return helper.GetImageIndex(autoRunEntry.FilePath);
            };
        }
        #endregion

        #region Importer Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        private void OnImporter_Complete(List<AutoRunEntry> entries)
        {
            MethodInvoker methodInvoker = delegate
            {
                foreach (ColumnHeader colHeader in listEntries.Columns)
                {
                    colHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                SetProcessingState(true);
                _hourGlass.Dispose();
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoRunEntry"></param>
        private void OnImporter_EntryFound(AutoRunEntry autoRunEntry)
        {
            listEntries.AddObject(autoRunEntry);
        }
        #endregion

        #region Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileImport_Click(object sender, EventArgs e)
        {
            using (FormImport formImport = new FormImport())
            {
                if (formImport.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                listEntries.ClearObjects();
                Helper.AutoResizeListColumns(listEntries);

                _hourGlass = new HourGlass(this);
                SetProcessingState(true);
                _importer.Start(_configuration, formImport.DriveMappings, formImport.RegistryPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpHelp_Click(object sender, EventArgs e)
        {
            Misc.ShellExecuteFile(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Help.pdf"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            using (FormAbout formAbout = new FormAbout())
            {
                formAbout.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExportAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "TSV|*.tsv|AllFiles|*.*";
                saveFileDialog.Title = "Select the export file...";
                if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                CsvConfiguration csvConfiguration = new CsvConfiguration();
                csvConfiguration.Delimiter = "\t";

                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                {
                    // Write out the file headers
                    csvWriter.WriteField("Type");
                    csvWriter.WriteField("Path");
                    csvWriter.WriteField("FilePath");
                    csvWriter.WriteField("FileName");
                    csvWriter.WriteField("Parameters");
                    csvWriter.WriteField("ServiceDisplayName");
                    csvWriter.WriteField("ServiceDescription");
                    csvWriter.WriteField("Exists");
                    csvWriter.WriteField("Verified");
                    csvWriter.WriteField("StrongName");
                    csvWriter.WriteField("Publisher");
                    csvWriter.WriteField("FileDate");
                    csvWriter.WriteField("SigningDate");
                    csvWriter.WriteField("Version");
                    csvWriter.WriteField("FileVersion");
                    csvWriter.WriteField("MD5");
                    csvWriter.WriteField("FileSystemCreated");
                    csvWriter.WriteField("FileSystemModified");
                    csvWriter.WriteField("FileSystemAccessed");
                    csvWriter.WriteField("RegistryModified");
                    csvWriter.WriteField("Description");
                    csvWriter.WriteField("Info");
                    csvWriter.WriteField("SourceFile");
                    csvWriter.NextRecord();

                    foreach (AutoRunEntry autoRunEntry in listEntries.Objects)
                    {
                        csvWriter.WriteField(autoRunEntry.Type);
                        csvWriter.WriteField(autoRunEntry.Path);
                        csvWriter.WriteField(autoRunEntry.FilePath);
                        csvWriter.WriteField(autoRunEntry.FileName);
                        csvWriter.WriteField(autoRunEntry.Parameters);
                        csvWriter.WriteField(autoRunEntry.ServiceDisplayName);
                        csvWriter.WriteField(autoRunEntry.ServiceDescription);
                        csvWriter.WriteField(autoRunEntry.Exists);
                        csvWriter.WriteField(autoRunEntry.Verified);
                        csvWriter.WriteField(autoRunEntry.StrongName);
                        csvWriter.WriteField(autoRunEntry.FilePublisher);
                        csvWriter.WriteField(autoRunEntry.FileDateText);
                        csvWriter.WriteField(autoRunEntry.SigningDateText);
                        csvWriter.WriteField(autoRunEntry.Version);
                        csvWriter.WriteField(autoRunEntry.FileVersion);
                        csvWriter.WriteField(autoRunEntry.Md5);
                        csvWriter.WriteField(autoRunEntry.FileSystemCreatedDateText);
                        csvWriter.WriteField(autoRunEntry.FileSystemModifiedDateText);
                        csvWriter.WriteField(autoRunEntry.FileSystemAccessedDateText);
                        csvWriter.WriteField(autoRunEntry.RegistryModifiedText);
                        csvWriter.WriteField(autoRunEntry.FileDescription);
                        csvWriter.WriteField(autoRunEntry.Info);
                        csvWriter.WriteField(autoRunEntry.SourceFile);
                        csvWriter.NextRecord();
                    }
                }

                UserInterface.DisplayMessageBox(this, "Export complete", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst exporting the data: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExportMd5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text|*.txt|AllFiles|*.*";
            saveFileDialog.Title = "Select the export file...";
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
                
            using (new HourGlass(this))
            {
                List<AutoRunEntry> autoRunEntries = listEntries.Objects.Cast<AutoRunEntry>().ToList();
                var temp = autoRunEntries.Select(i => i.Md5).Distinct().ToList();
                var uniqueMd5s = (from a in temp where a.Length > 0 select a).ToList();
                uniqueMd5s.Sort();

                IO.WriteTextToFile(string.Join(Environment.NewLine, uniqueMd5s), saveFileDialog.FileName, false);

                UserInterface.DisplayMessageBox(this, "Export complete", MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Toolbar Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnImport_Click(object sender, EventArgs e)
        {
            menuFileImport_Click(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnExport_Click(object sender, EventArgs e)
        {
            menuFileExportAll_Click(this, new EventArgs());
        }
        #endregion 

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Tools", "sigcheck.exe")) == false)
            {
                DialogResult dialogResult = UserInterface.DisplayQuestionMessageBox(this, "The Sysinternals sigcheck tool does not exist in the Tools directory. The application cannot run without it. Do you want to download the zip file?");
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    Application.Exit();
                    return;
                }

                WebClient wc = new WebClient();
                byte[] data = wc.DownloadData("http://download.sysinternals.com/files/Sigcheck.zip");
                using (ZipFile zipFile = ZipFile.Read(new MemoryStream(data)))
                {
                    foreach (ZipEntry zipEntry in zipFile)
                    {
                        if (zipEntry.FileName.ToLower().Equals("sigcheck.exe") == false)
                        {
                            continue;
                        }

                        zipEntry.Extract(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Tools"), ExtractExistingFileAction.OverwriteSilently);
                    }
                }

                if (File.Exists(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Tools", "sigcheck.exe")) == false)
                {
                    UserInterface.DisplayErrorMessageBox(this, "Unable to download sigcheck tool. Download manually and copy to the Tools directory");
                    Application.Exit();
                    return;
                }
                else
                {
                    UserInterface.DisplayMessageBox(this, "The sigcheck tool was successfully downloaded", MessageBoxIcon.Information);
                }
            }

            _configuration = new Configuration();
            string ret = _configuration.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to load configuration, the application will exit: " + ret);
                Application.Exit();
                return;
            }

            _settings = new Settings();
            ret = _settings.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to load settings: " + ret);
            }

            Helper.AutoResizeListColumns(listEntries);
        }
        #endregion

        #region User Interface Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void UpdateStatusBar(string text)
        {
            MethodInvoker methodInvoker = delegate
            {
                statusLabel.Text = text;
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void SetProcessingState(bool enabled)
        {
            MethodInvoker methodInvoker = delegate
            {
                menuFileImport.Enabled = enabled;
                menuFileExport.Enabled = enabled;
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }
        #endregion

        #region Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyType_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Type, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyPath_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Path, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyFilePath_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.FilePath, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyFileName_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.FileName, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyParameters_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Parameters, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyPublisher_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Publisher, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyFileDate_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.FileDate, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopySigningDate_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.SigningDate, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyVersion_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Version, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyMd5_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Md5, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyDescription_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Description, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextCopyInfo_Click(object sender, EventArgs e)
        {
            AutoRunEntry entry = (AutoRunEntry)listEntries.SelectedObject;
            string ret = Helper.CopyToClipboard(Global.Columns.Info, entry);
            UpdateStatusBar(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listEntries.SelectedObjects.Count != 1)
            {
                contextCopy.Enabled = false;
                return;
            }

            contextCopy.Enabled = true;
        }
        #endregion
    }
}
