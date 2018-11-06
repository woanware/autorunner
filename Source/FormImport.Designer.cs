namespace autorunner
{
    partial class FormImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImport));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRegistryPath = new System.Windows.Forms.Label();
            this.txtRegistryPath = new System.Windows.Forms.TextBox();
            this.listDriveMappings = new BrightIdeasSoftware.ObjectListView();
            this.olvcOrig = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcMapped = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcWindowsDrive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtCatalogPath = new System.Windows.Forms.TextBox();
            this.lblCatalogPath = new System.Windows.Forms.Label();
            this.btnRegistryPath = new System.Windows.Forms.Button();
            this.btnCatalogPath = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listDriveMappings)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(692, 421);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(771, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRegistryPath
            // 
            this.lblRegistryPath.AutoSize = true;
            this.lblRegistryPath.Location = new System.Drawing.Point(15, 14);
            this.lblRegistryPath.Name = "lblRegistryPath";
            this.lblRegistryPath.Size = new System.Drawing.Size(76, 15);
            this.lblRegistryPath.TabIndex = 0;
            this.lblRegistryPath.Text = "Registry Path";
            // 
            // txtRegistryPath
            // 
            this.txtRegistryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistryPath.Location = new System.Drawing.Point(95, 10);
            this.txtRegistryPath.Name = "txtRegistryPath";
            this.txtRegistryPath.Size = new System.Drawing.Size(719, 23);
            this.txtRegistryPath.TabIndex = 1;
            // 
            // listDriveMappings
            // 
            this.listDriveMappings.AllColumns.Add(this.olvcOrig);
            this.listDriveMappings.AllColumns.Add(this.olvcMapped);
            this.listDriveMappings.AllColumns.Add(this.olvcWindowsDrive);
            this.listDriveMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listDriveMappings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcOrig,
            this.olvcMapped,
            this.olvcWindowsDrive});
            this.listDriveMappings.FullRowSelect = true;
            this.listDriveMappings.HideSelection = false;
            this.listDriveMappings.Location = new System.Drawing.Point(9, 75);
            this.listDriveMappings.MultiSelect = false;
            this.listDriveMappings.Name = "listDriveMappings";
            this.listDriveMappings.ShowGroups = false;
            this.listDriveMappings.Size = new System.Drawing.Size(836, 308);
            this.listDriveMappings.TabIndex = 4;
            this.listDriveMappings.UseCompatibleStateImageBehavior = false;
            this.listDriveMappings.View = System.Windows.Forms.View.Details;
            this.listDriveMappings.SelectedIndexChanged += new System.EventHandler(this.listDriveMappings_SelectedIndexChanged);
            this.listDriveMappings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listDriveMappings_MouseDoubleClick);
            // 
            // olvcOrig
            // 
            this.olvcOrig.AspectName = "OriginalDrive";
            this.olvcOrig.Text = "Original Drive";
            this.olvcOrig.Width = 124;
            // 
            // olvcMapped
            // 
            this.olvcMapped.AspectName = "MappedDrive";
            this.olvcMapped.Text = "Mapped Drive";
            this.olvcMapped.Width = 113;
            // 
            // olvcWindowsDrive
            // 
            this.olvcWindowsDrive.AspectName = "IsWindowsDrive";
            this.olvcWindowsDrive.Text = "Is Windows Drive";
            this.olvcWindowsDrive.Width = 135;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(8, 389);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(37, 389);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(25, 25);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(66, 389);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 25);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtCatalogPath
            // 
            this.txtCatalogPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCatalogPath.Location = new System.Drawing.Point(95, 41);
            this.txtCatalogPath.Name = "txtCatalogPath";
            this.txtCatalogPath.Size = new System.Drawing.Size(719, 23);
            this.txtCatalogPath.TabIndex = 3;
            // 
            // lblCatalogPath
            // 
            this.lblCatalogPath.AutoSize = true;
            this.lblCatalogPath.Location = new System.Drawing.Point(15, 45);
            this.lblCatalogPath.Name = "lblCatalogPath";
            this.lblCatalogPath.Size = new System.Drawing.Size(75, 15);
            this.lblCatalogPath.TabIndex = 2;
            this.lblCatalogPath.Text = "Catalog Path";
            // 
            // btnRegistryPath
            // 
            this.btnRegistryPath.Location = new System.Drawing.Point(822, 9);
            this.btnRegistryPath.Name = "btnRegistryPath";
            this.btnRegistryPath.Size = new System.Drawing.Size(24, 24);
            this.btnRegistryPath.TabIndex = 10;
            this.btnRegistryPath.Text = "...";
            this.btnRegistryPath.UseVisualStyleBackColor = true;
            this.btnRegistryPath.Click += new System.EventHandler(this.btnRegistryPath_Click);
            // 
            // btnCatalogPath
            // 
            this.btnCatalogPath.Location = new System.Drawing.Point(821, 41);
            this.btnCatalogPath.Name = "btnCatalogPath";
            this.btnCatalogPath.Size = new System.Drawing.Size(24, 24);
            this.btnCatalogPath.TabIndex = 11;
            this.btnCatalogPath.Text = "...";
            this.btnCatalogPath.UseVisualStyleBackColor = true;
            this.btnCatalogPath.Click += new System.EventHandler(this.btnCatalogPath_Click);
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(855, 453);
            this.Controls.Add(this.btnCatalogPath);
            this.Controls.Add(this.btnRegistryPath);
            this.Controls.Add(this.txtCatalogPath);
            this.Controls.Add(this.lblCatalogPath);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listDriveMappings);
            this.Controls.Add(this.txtRegistryPath);
            this.Controls.Add(this.lblRegistryPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(384, 277);
            this.Name = "FormImport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            ((System.ComponentModel.ISupportInitialize)(this.listDriveMappings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRegistryPath;
        private System.Windows.Forms.TextBox txtRegistryPath;
        private BrightIdeasSoftware.ObjectListView listDriveMappings;
        private BrightIdeasSoftware.OLVColumn olvcOrig;
        private BrightIdeasSoftware.OLVColumn olvcMapped;
        private BrightIdeasSoftware.OLVColumn olvcWindowsDrive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtCatalogPath;
        private System.Windows.Forms.Label lblCatalogPath;
        private System.Windows.Forms.Button btnRegistryPath;
        private System.Windows.Forms.Button btnCatalogPath;
    }
}