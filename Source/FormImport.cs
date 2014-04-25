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
    public partial class FormImport : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormImport()
        {
            InitializeComponent();
            SetListViewButtonStatus();
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (txtRegistryPath.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The path to the directory containing the registry hives must be entered", MessageBoxIcon.Exclamation);
                txtRegistryPath.Select();
                return;
            }

            if (listDriveMappings.Items.Count == 0)
            {
                UserInterface.DisplayMessageBox(this, "At least one drive mapping must be supplied", MessageBoxIcon.Exclamation);
                btnAdd.Select();
                return;
            }

            List<DriveMapping> driveMappings = listDriveMappings.Objects.Cast<DriveMapping>().ToList();
            var windowsDrive = from d in driveMappings where d.IsWindowsDrive == true select d;
            if (windowsDrive.Count() == 0)
            {
                UserInterface.DisplayMessageBox(this, "At least one drive mapping must be set as the Windows drive", MessageBoxIcon.Exclamation);
                btnEdit.Select();
                return;
            }
            else if (windowsDrive.Count() > 1)
            {
                UserInterface.DisplayMessageBox(this, "Only one drive can be set as the Windows drive", MessageBoxIcon.Exclamation);
                btnEdit.Select();
                return;
            }

            foreach (DriveMapping dm in driveMappings)
            {
                var count = (from d in driveMappings where d.OriginalDrive.ToLower() == dm.OriginalDrive.ToLower() select d).Count();
                if (count > 1)
                {
                    UserInterface.DisplayMessageBox(this, "There are duplicate original drive mappings", MessageBoxIcon.Exclamation);
                    btnEdit.Select();
                    return;
                }
            }

            foreach (DriveMapping dm in driveMappings)
            {
                var count = (from d in driveMappings where d.MappedDrive.ToLower() == dm.MappedDrive.ToLower() select d).Count();
                if (count > 1)
                {
                    UserInterface.DisplayMessageBox(this, "There are duplicate mapped drive mappings", MessageBoxIcon.Exclamation);
                    btnEdit.Select();
                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            using (FormDriveMapping form = new FormDriveMapping())
            {
                if (form.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                listDriveMappings.AddObject(form.DriveMapping);
                listDriveMappings.SelectedObject = form.DriveMapping;

                SetListViewButtonStatus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            if (listDriveMappings.SelectedObjects.Count == 0)
            {
                return;
            }

            DriveMapping dm = (DriveMapping)listDriveMappings.SelectedObjects[0];
            using (FormDriveMapping form = new FormDriveMapping())
            {
                form.DriveMapping = dm;
                if (form.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                dm.OriginalDrive = form.DriveMapping.OriginalDrive;
                dm.MappedDrive = form.DriveMapping.MappedDrive;
                dm.IsWindowsDrive = form.DriveMapping.IsWindowsDrive;
                listDriveMappings.RefreshObject(dm);

                SetListViewButtonStatus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (listDriveMappings.SelectedObjects.Count == 0)
            {
                return;
            }

            DialogResult dr = UserInterface.DisplayQuestionMessageBox(this, "Are you sure you want to delete the mapping?");
            if (dr != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            DriveMapping dm = (DriveMapping)listDriveMappings.SelectedObjects[0];
            listDriveMappings.RemoveObject(dm);

            SetListViewButtonStatus();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string RegistryPath
        {
            get
            {
                return txtRegistryPath.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<DriveMapping> DriveMappings
        {
            get
            {
                List<DriveMapping> driveMappings = listDriveMappings.Objects.Cast<DriveMapping>().ToList();
                return driveMappings;
            }
        }
        #endregion        

        #region List Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listDriveMappings_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SetListViewButtonStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listDriveMappings_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click(this, new EventArgs());
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void SetListViewButtonStatus()
        {
            if (listDriveMappings.SelectedObjects.Count == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }

            olvcOrig.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            olvcMapped.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            olvcWindowsDrive.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
