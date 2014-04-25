using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using woanware;

namespace autorunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormDriveMapping : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormDriveMapping()
        {
            InitializeComponent();

            List<NameValue> drives = new List<NameValue>();
            for (int index = 65; index < 91; index++)
            {
                NameValue nameValue = new NameValue();
                nameValue.Name = Convert.ToChar(index) + @":\";
                nameValue.Value = Convert.ToChar(index) + @":\";

                drives.Add(nameValue);
            }

            cboOriginalDrive.DisplayMember = "Name";
            cboOriginalDrive.ValueMember = "Value";
            cboOriginalDrive.Items.AddRange(drives.ToArray());

            if (drives.Count > 0)
            {
                cboOriginalDrive.SelectedIndex = 0;
            }

            RefreshDrives();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        private void RefreshDrives()
        {
            using (new HourGlass(this))
            {
                var drives = System.IO.DriveInfo.GetDrives().Where(d => d.DriveType == System.IO.DriveType.Fixed);

                List<NameValue> temp = new List<NameValue>();
                foreach (DriveInfo driveInfo in drives)
                {
                    if (driveInfo.IsReady == false)
                    {
                        continue;
                    }

                    try
                    {
                        NameValue nameValue = new NameValue();
                        nameValue.Name = driveInfo.Name + " (" + driveInfo.DriveFormat + ")";
                        nameValue.Value = driveInfo.Name;

                        temp.Add(nameValue);
                    }
                    catch (Exception) { }
                }

                cboMappedDrive.DisplayMember = "Name";
                cboMappedDrive.ValueMember = "Value";
                cboMappedDrive.Items.AddRange(temp.ToArray());

                if (temp.Count > 0)
                {
                    cboMappedDrive.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cboOriginalDrive.SelectedIndex == -1)
            {
                return;
            }

            if (cboMappedDrive.SelectedIndex == -1)
            {
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DriveMapping DriveMapping
        {
            get
            {
                DriveMapping dm = new autorunner.DriveMapping();

                NameValue nameValue = (NameValue)cboMappedDrive.Items[cboMappedDrive.SelectedIndex];
                dm.MappedDrive = nameValue.Value;
                nameValue = (NameValue)cboOriginalDrive.Items[cboOriginalDrive.SelectedIndex];
                dm.OriginalDrive = nameValue.Value;
                dm.IsWindowsDrive = chkIsWindowsDrive.Checked;

                return dm;
            }
            set
            {
                DriveMapping dm = value;
                UserInterface.LocateAndSelectNameValueCombo(cboOriginalDrive, dm.OriginalDrive, false);
                UserInterface.LocateAndSelectNameValueCombo(cboMappedDrive, dm.MappedDrive, false);
                chkIsWindowsDrive.Checked = dm.IsWindowsDrive;
            }
        }
        #endregion
    }
}
