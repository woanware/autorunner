namespace autorunner
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExportMd5 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolBtnImport = new System.Windows.Forms.ToolStripButton();
            this.toolBtnExport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.listEntries = new BrightIdeasSoftware.ObjectListView();
            this.olvcType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPath = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFilePath = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFileName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcParameters = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcServiceDisplayName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcServiceDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExists = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcVerified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcStrongName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPublisher = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFileDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSigningDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcVersion = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcMd5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFileSystemCreated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFileSystemAccessed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFileSystemModified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcRegistryModifed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcInfo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSourceFile = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyType = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyPath = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyFilePath = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyPublisher = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyFileDate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopySigningDate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyMd5 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyDescription = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopyInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEntries)).BeginInit();
            this.context.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(1346, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileImport,
            this.menuFileSep1,
            this.menuFileExport,
            this.menuFileSep2,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuFileImport
            // 
            this.menuFileImport.Name = "menuFileImport";
            this.menuFileImport.Size = new System.Drawing.Size(110, 22);
            this.menuFileImport.Text = "Import";
            this.menuFileImport.Click += new System.EventHandler(this.menuFileImport_Click);
            // 
            // menuFileSep1
            // 
            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(107, 6);
            // 
            // menuFileExport
            // 
            this.menuFileExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExportAll,
            this.menuFileExportMd5});
            this.menuFileExport.Name = "menuFileExport";
            this.menuFileExport.Size = new System.Drawing.Size(110, 22);
            this.menuFileExport.Text = "Export";
            // 
            // menuFileExportAll
            // 
            this.menuFileExportAll.Name = "menuFileExportAll";
            this.menuFileExportAll.Size = new System.Drawing.Size(99, 22);
            this.menuFileExportAll.Text = "All";
            this.menuFileExportAll.Click += new System.EventHandler(this.menuFileExportAll_Click);
            // 
            // menuFileExportMd5
            // 
            this.menuFileExportMd5.Name = "menuFileExportMd5";
            this.menuFileExportMd5.Size = new System.Drawing.Size(99, 22);
            this.menuFileExportMd5.Text = "MD5";
            this.menuFileExportMd5.Click += new System.EventHandler(this.menuFileExportMd5_Click);
            // 
            // menuFileSep2
            // 
            this.menuFileSep2.Name = "menuFileSep2";
            this.menuFileSep2.Size = new System.Drawing.Size(107, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(110, 22);
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpHelp,
            this.menuHelpSep1,
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "Help";
            // 
            // menuHelpHelp
            // 
            this.menuHelpHelp.Name = "menuHelpHelp";
            this.menuHelpHelp.Size = new System.Drawing.Size(107, 22);
            this.menuHelpHelp.Text = "Help";
            this.menuHelpHelp.Click += new System.EventHandler(this.menuHelpHelp_Click);
            // 
            // menuHelpSep1
            // 
            this.menuHelpSep1.Name = "menuHelpSep1";
            this.menuHelpSep1.Size = new System.Drawing.Size(104, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.menuHelpAbout.Text = "About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnImport,
            this.toolBtnExport});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(1346, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolBtnImport
            // 
            this.toolBtnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnImport.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnImport.Image")));
            this.toolBtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnImport.Name = "toolBtnImport";
            this.toolBtnImport.Size = new System.Drawing.Size(23, 22);
            this.toolBtnImport.ToolTipText = "Import";
            this.toolBtnImport.Click += new System.EventHandler(this.toolBtnImport_Click);
            // 
            // toolBtnExport
            // 
            this.toolBtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnExport.Image")));
            this.toolBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnExport.Name = "toolBtnExport";
            this.toolBtnExport.Size = new System.Drawing.Size(23, 22);
            this.toolBtnExport.ToolTipText = "Export";
            this.toolBtnExport.Click += new System.EventHandler(this.toolBtnExport_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 293);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1346, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // listEntries
            // 
            this.listEntries.AllColumns.Add(this.olvcType);
            this.listEntries.AllColumns.Add(this.olvcPath);
            this.listEntries.AllColumns.Add(this.olvcFilePath);
            this.listEntries.AllColumns.Add(this.olvcFileName);
            this.listEntries.AllColumns.Add(this.olvcParameters);
            this.listEntries.AllColumns.Add(this.olvcServiceDisplayName);
            this.listEntries.AllColumns.Add(this.olvcServiceDescription);
            this.listEntries.AllColumns.Add(this.olvcExists);
            this.listEntries.AllColumns.Add(this.olvcVerified);
            this.listEntries.AllColumns.Add(this.olvcStrongName);
            this.listEntries.AllColumns.Add(this.olvcPublisher);
            this.listEntries.AllColumns.Add(this.olvcFileDate);
            this.listEntries.AllColumns.Add(this.olvcSigningDate);
            this.listEntries.AllColumns.Add(this.olvcVersion);
            this.listEntries.AllColumns.Add(this.olvcMd5);
            this.listEntries.AllColumns.Add(this.olvcFileSystemCreated);
            this.listEntries.AllColumns.Add(this.olvcFileSystemAccessed);
            this.listEntries.AllColumns.Add(this.olvcFileSystemModified);
            this.listEntries.AllColumns.Add(this.olvcRegistryModifed);
            this.listEntries.AllColumns.Add(this.olvcDescription);
            this.listEntries.AllColumns.Add(this.olvcInfo);
            this.listEntries.AllColumns.Add(this.olvcSourceFile);
            this.listEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcType,
            this.olvcPath,
            this.olvcFilePath,
            this.olvcFileName,
            this.olvcParameters,
            this.olvcServiceDisplayName,
            this.olvcServiceDescription,
            this.olvcExists,
            this.olvcVerified,
            this.olvcStrongName,
            this.olvcPublisher,
            this.olvcFileDate,
            this.olvcSigningDate,
            this.olvcVersion,
            this.olvcMd5,
            this.olvcFileSystemCreated,
            this.olvcFileSystemAccessed,
            this.olvcFileSystemModified,
            this.olvcRegistryModifed,
            this.olvcDescription,
            this.olvcInfo,
            this.olvcSourceFile});
            this.listEntries.ContextMenuStrip = this.context;
            this.listEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEntries.FullRowSelect = true;
            this.listEntries.HideSelection = false;
            this.listEntries.Location = new System.Drawing.Point(0, 49);
            this.listEntries.MultiSelect = false;
            this.listEntries.Name = "listEntries";
            this.listEntries.OwnerDraw = true;
            this.listEntries.ShowGroups = false;
            this.listEntries.Size = new System.Drawing.Size(1346, 244);
            this.listEntries.SmallImageList = this.imageList;
            this.listEntries.TabIndex = 3;
            this.listEntries.UseCompatibleStateImageBehavior = false;
            this.listEntries.UseFiltering = true;
            this.listEntries.View = System.Windows.Forms.View.Details;
            // 
            // olvcType
            // 
            this.olvcType.AspectName = "Type";
            this.olvcType.CellPadding = null;
            this.olvcType.IsTileViewColumn = true;
            this.olvcType.Text = "Type";
            // 
            // olvcPath
            // 
            this.olvcPath.AspectName = "Path";
            this.olvcPath.CellPadding = null;
            this.olvcPath.Text = "Path";
            // 
            // olvcFilePath
            // 
            this.olvcFilePath.AspectName = "FilePath";
            this.olvcFilePath.CellPadding = null;
            this.olvcFilePath.Text = "File Path";
            // 
            // olvcFileName
            // 
            this.olvcFileName.AspectName = "FileName";
            this.olvcFileName.CellPadding = null;
            this.olvcFileName.Text = "File Name";
            // 
            // olvcParameters
            // 
            this.olvcParameters.AspectName = "Parameters";
            this.olvcParameters.CellPadding = null;
            this.olvcParameters.Text = "Parameters";
            // 
            // olvcServiceDisplayName
            // 
            this.olvcServiceDisplayName.AspectName = "ServiceDisplayName";
            this.olvcServiceDisplayName.CellPadding = null;
            this.olvcServiceDisplayName.Text = "Service Display Name";
            this.olvcServiceDisplayName.Width = 62;
            // 
            // olvcServiceDescription
            // 
            this.olvcServiceDescription.AspectName = "ServiceDescription";
            this.olvcServiceDescription.CellPadding = null;
            this.olvcServiceDescription.Text = "Service Description";
            this.olvcServiceDescription.Width = 129;
            // 
            // olvcExists
            // 
            this.olvcExists.AspectName = "Exists";
            this.olvcExists.CellPadding = null;
            this.olvcExists.Text = "Exists";
            // 
            // olvcVerified
            // 
            this.olvcVerified.AspectName = "Verified";
            this.olvcVerified.CellPadding = null;
            this.olvcVerified.Text = "Verified";
            // 
            // olvcStrongName
            // 
            this.olvcStrongName.AspectName = "StrongName";
            this.olvcStrongName.CellPadding = null;
            this.olvcStrongName.Text = "Strong Named";
            this.olvcStrongName.Width = 59;
            // 
            // olvcPublisher
            // 
            this.olvcPublisher.AspectName = "FilePublisher";
            this.olvcPublisher.CellPadding = null;
            this.olvcPublisher.Text = "Publisher";
            // 
            // olvcFileDate
            // 
            this.olvcFileDate.AspectName = "FileDateText";
            this.olvcFileDate.CellPadding = null;
            this.olvcFileDate.Text = "File Date";
            // 
            // olvcSigningDate
            // 
            this.olvcSigningDate.AspectName = "SigningDateText";
            this.olvcSigningDate.CellPadding = null;
            this.olvcSigningDate.Text = "Signing Date";
            this.olvcSigningDate.Width = 94;
            // 
            // olvcVersion
            // 
            this.olvcVersion.AspectName = "Version";
            this.olvcVersion.CellPadding = null;
            this.olvcVersion.Text = "Version";
            // 
            // olvcMd5
            // 
            this.olvcMd5.AspectName = "Md5";
            this.olvcMd5.CellPadding = null;
            this.olvcMd5.Text = "MD5";
            // 
            // olvcFileSystemCreated
            // 
            this.olvcFileSystemCreated.AspectName = "FileSystemCreated";
            this.olvcFileSystemCreated.CellPadding = null;
            this.olvcFileSystemCreated.Text = "File System Created";
            // 
            // olvcFileSystemAccessed
            // 
            this.olvcFileSystemAccessed.AspectName = "FileSystemAccessed";
            this.olvcFileSystemAccessed.CellPadding = null;
            this.olvcFileSystemAccessed.Text = "File System Accessed";
            // 
            // olvcFileSystemModified
            // 
            this.olvcFileSystemModified.AspectName = "FileSystemModified";
            this.olvcFileSystemModified.CellPadding = null;
            this.olvcFileSystemModified.Text = "File System Modified";
            // 
            // olvcRegistryModifed
            // 
            this.olvcRegistryModifed.AspectName = "RegistryModified";
            this.olvcRegistryModifed.CellPadding = null;
            this.olvcRegistryModifed.Text = "Registry Modified";
            // 
            // olvcDescription
            // 
            this.olvcDescription.AspectName = "FileDescription";
            this.olvcDescription.CellPadding = null;
            this.olvcDescription.Text = "Description";
            // 
            // olvcInfo
            // 
            this.olvcInfo.AspectName = "Info";
            this.olvcInfo.CellPadding = null;
            this.olvcInfo.Text = "Info";
            // 
            // olvcSourceFile
            // 
            this.olvcSourceFile.AspectName = "SourceFile";
            this.olvcSourceFile.CellPadding = null;
            this.olvcSourceFile.Text = "Source File";
            // 
            // context
            // 
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCopy});
            this.context.Name = "context";
            this.context.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.context.Size = new System.Drawing.Size(103, 26);
            this.context.Opening += new System.ComponentModel.CancelEventHandler(this.context_Opening);
            // 
            // contextCopy
            // 
            this.contextCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCopyType,
            this.contextCopyPath,
            this.contextCopyFilePath,
            this.contextCopyFileName,
            this.contextCopyParameters,
            this.contextCopyPublisher,
            this.contextCopyFileDate,
            this.contextCopySigningDate,
            this.contextCopyVersion,
            this.contextCopyMd5,
            this.contextCopyDescription,
            this.contextCopyInfo});
            this.contextCopy.Name = "contextCopy";
            this.contextCopy.Size = new System.Drawing.Size(102, 22);
            this.contextCopy.Text = "Copy";
            // 
            // contextCopyType
            // 
            this.contextCopyType.Name = "contextCopyType";
            this.contextCopyType.Size = new System.Drawing.Size(141, 22);
            this.contextCopyType.Text = "Type";
            this.contextCopyType.Click += new System.EventHandler(this.contextCopyType_Click);
            // 
            // contextCopyPath
            // 
            this.contextCopyPath.Name = "contextCopyPath";
            this.contextCopyPath.Size = new System.Drawing.Size(141, 22);
            this.contextCopyPath.Text = "Path";
            this.contextCopyPath.Click += new System.EventHandler(this.contextCopyPath_Click);
            // 
            // contextCopyFilePath
            // 
            this.contextCopyFilePath.Name = "contextCopyFilePath";
            this.contextCopyFilePath.Size = new System.Drawing.Size(141, 22);
            this.contextCopyFilePath.Text = "File Path";
            this.contextCopyFilePath.Click += new System.EventHandler(this.contextCopyFilePath_Click);
            // 
            // contextCopyFileName
            // 
            this.contextCopyFileName.Name = "contextCopyFileName";
            this.contextCopyFileName.Size = new System.Drawing.Size(141, 22);
            this.contextCopyFileName.Text = "File Name";
            this.contextCopyFileName.Click += new System.EventHandler(this.contextCopyFileName_Click);
            // 
            // contextCopyParameters
            // 
            this.contextCopyParameters.Name = "contextCopyParameters";
            this.contextCopyParameters.Size = new System.Drawing.Size(141, 22);
            this.contextCopyParameters.Text = "Parameters";
            this.contextCopyParameters.Click += new System.EventHandler(this.contextCopyParameters_Click);
            // 
            // contextCopyPublisher
            // 
            this.contextCopyPublisher.Name = "contextCopyPublisher";
            this.contextCopyPublisher.Size = new System.Drawing.Size(141, 22);
            this.contextCopyPublisher.Text = "Publisher";
            this.contextCopyPublisher.Click += new System.EventHandler(this.contextCopyPublisher_Click);
            // 
            // contextCopyFileDate
            // 
            this.contextCopyFileDate.Name = "contextCopyFileDate";
            this.contextCopyFileDate.Size = new System.Drawing.Size(141, 22);
            this.contextCopyFileDate.Text = "File Date";
            this.contextCopyFileDate.Click += new System.EventHandler(this.contextCopyFileDate_Click);
            // 
            // contextCopySigningDate
            // 
            this.contextCopySigningDate.Name = "contextCopySigningDate";
            this.contextCopySigningDate.Size = new System.Drawing.Size(141, 22);
            this.contextCopySigningDate.Text = "Signing Date";
            this.contextCopySigningDate.Click += new System.EventHandler(this.contextCopySigningDate_Click);
            // 
            // contextCopyVersion
            // 
            this.contextCopyVersion.Name = "contextCopyVersion";
            this.contextCopyVersion.Size = new System.Drawing.Size(141, 22);
            this.contextCopyVersion.Text = "Version";
            this.contextCopyVersion.Click += new System.EventHandler(this.contextCopyVersion_Click);
            // 
            // contextCopyMd5
            // 
            this.contextCopyMd5.Name = "contextCopyMd5";
            this.contextCopyMd5.Size = new System.Drawing.Size(141, 22);
            this.contextCopyMd5.Text = "MD5";
            this.contextCopyMd5.Click += new System.EventHandler(this.contextCopyMd5_Click);
            // 
            // contextCopyDescription
            // 
            this.contextCopyDescription.Name = "contextCopyDescription";
            this.contextCopyDescription.Size = new System.Drawing.Size(141, 22);
            this.contextCopyDescription.Text = "Description";
            this.contextCopyDescription.Click += new System.EventHandler(this.contextCopyDescription_Click);
            // 
            // contextCopyInfo
            // 
            this.contextCopyInfo.Name = "contextCopyInfo";
            this.contextCopyInfo.Size = new System.Drawing.Size(141, 22);
            this.contextCopyInfo.Text = "Info";
            this.contextCopyInfo.Click += new System.EventHandler(this.contextCopyInfo_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 315);
            this.Controls.Add(this.listEntries);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "autorunner";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEntries)).EndInit();
            this.context.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private BrightIdeasSoftware.ObjectListView listEntries;
        private BrightIdeasSoftware.OLVColumn olvcType;
        private BrightIdeasSoftware.OLVColumn olvcPath;
        private BrightIdeasSoftware.OLVColumn olvcFileName;
        private BrightIdeasSoftware.OLVColumn olvcParameters;
        private BrightIdeasSoftware.OLVColumn olvcExists;
        private BrightIdeasSoftware.OLVColumn olvcVerified;
        private BrightIdeasSoftware.OLVColumn olvcStrongName;
        private BrightIdeasSoftware.OLVColumn olvcPublisher;
        private BrightIdeasSoftware.OLVColumn olvcFileDate;
        private BrightIdeasSoftware.OLVColumn olvcVersion;
        private BrightIdeasSoftware.OLVColumn olvcDescription;
        private BrightIdeasSoftware.OLVColumn olvcSigningDate;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuFileImport;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripButton toolBtnImport;
        private System.Windows.Forms.ToolStripMenuItem menuFileExport;
        private System.Windows.Forms.ToolStripSeparator menuFileSep2;
        private System.Windows.Forms.ToolStripMenuItem menuHelpHelp;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep1;
        private BrightIdeasSoftware.OLVColumn olvcInfo;
        private BrightIdeasSoftware.OLVColumn olvcFilePath;
        private System.Windows.Forms.ImageList imageList;
        private BrightIdeasSoftware.OLVColumn olvcMd5;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripButton toolBtnExport;
        private System.Windows.Forms.ContextMenuStrip context;
        private System.Windows.Forms.ToolStripMenuItem contextCopy;
        private System.Windows.Forms.ToolStripMenuItem contextCopyType;
        private System.Windows.Forms.ToolStripMenuItem contextCopyPath;
        private System.Windows.Forms.ToolStripMenuItem contextCopyFilePath;
        private System.Windows.Forms.ToolStripMenuItem contextCopyFileName;
        private System.Windows.Forms.ToolStripMenuItem contextCopyParameters;
        private System.Windows.Forms.ToolStripMenuItem contextCopyPublisher;
        private System.Windows.Forms.ToolStripMenuItem contextCopyFileDate;
        private System.Windows.Forms.ToolStripMenuItem contextCopySigningDate;
        private System.Windows.Forms.ToolStripMenuItem contextCopyVersion;
        private System.Windows.Forms.ToolStripMenuItem contextCopyMd5;
        private System.Windows.Forms.ToolStripMenuItem contextCopyDescription;
        private System.Windows.Forms.ToolStripMenuItem contextCopyInfo;
        private System.Windows.Forms.ToolStripMenuItem menuFileExportAll;
        private System.Windows.Forms.ToolStripMenuItem menuFileExportMd5;
        private BrightIdeasSoftware.OLVColumn olvcFileSystemCreated;
        private BrightIdeasSoftware.OLVColumn olvcFileSystemAccessed;
        private BrightIdeasSoftware.OLVColumn olvcFileSystemModified;
        private BrightIdeasSoftware.OLVColumn olvcRegistryModifed;
        private BrightIdeasSoftware.OLVColumn olvcSourceFile;
        private BrightIdeasSoftware.OLVColumn olvcServiceDisplayName;
        private BrightIdeasSoftware.OLVColumn olvcServiceDescription;
    }
}

