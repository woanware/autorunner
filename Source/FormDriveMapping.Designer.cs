namespace autorunner
{
    partial class FormDriveMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDriveMapping));
            this.cboOriginalDrive = new System.Windows.Forms.ComboBox();
            this.cboMappedDrive = new System.Windows.Forms.ComboBox();
            this.chkIsWindowsDrive = new System.Windows.Forms.CheckBox();
            this.lblMappedDrive = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboOriginalDrive
            // 
            this.cboOriginalDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOriginalDrive.FormattingEnabled = true;
            this.cboOriginalDrive.Location = new System.Drawing.Point(108, 12);
            this.cboOriginalDrive.Name = "cboOriginalDrive";
            this.cboOriginalDrive.Size = new System.Drawing.Size(185, 23);
            this.cboOriginalDrive.TabIndex = 1;
            // 
            // cboMappedDrive
            // 
            this.cboMappedDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMappedDrive.FormattingEnabled = true;
            this.cboMappedDrive.Location = new System.Drawing.Point(108, 42);
            this.cboMappedDrive.Name = "cboMappedDrive";
            this.cboMappedDrive.Size = new System.Drawing.Size(185, 23);
            this.cboMappedDrive.TabIndex = 3;
            // 
            // chkIsWindowsDrive
            // 
            this.chkIsWindowsDrive.AutoSize = true;
            this.chkIsWindowsDrive.Location = new System.Drawing.Point(7, 71);
            this.chkIsWindowsDrive.Name = "chkIsWindowsDrive";
            this.chkIsWindowsDrive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkIsWindowsDrive.Size = new System.Drawing.Size(116, 19);
            this.chkIsWindowsDrive.TabIndex = 4;
            this.chkIsWindowsDrive.Text = "Is Windows Drive";
            this.chkIsWindowsDrive.UseVisualStyleBackColor = true;
            // 
            // lblMappedDrive
            // 
            this.lblMappedDrive.AutoSize = true;
            this.lblMappedDrive.Location = new System.Drawing.Point(23, 46);
            this.lblMappedDrive.Name = "lblMappedDrive";
            this.lblMappedDrive.Size = new System.Drawing.Size(81, 15);
            this.lblMappedDrive.TabIndex = 2;
            this.lblMappedDrive.Text = "Mapped Drive";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original Drive";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(221, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(142, 97);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormDriveMapping
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(307, 128);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMappedDrive);
            this.Controls.Add(this.chkIsWindowsDrive);
            this.Controls.Add(this.cboMappedDrive);
            this.Controls.Add(this.cboOriginalDrive);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDriveMapping";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Drive Mapping";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboOriginalDrive;
        private System.Windows.Forms.ComboBox cboMappedDrive;
        private System.Windows.Forms.CheckBox chkIsWindowsDrive;
        private System.Windows.Forms.Label lblMappedDrive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}