using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autorunner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
          //  List<DriveMapping> dms = new List<DriveMapping>();
          //  DriveMapping dm = new DriveMapping();
          //  dm.MappedDrive = @"c:\";
          //  dm.OriginalDrive = @"c:\";
          //  dm.IsWindowsDrive = true;
          //  dms.Add(dm);

          //AutoRunEntry autoRunEntry = new AutoRunEntry();
          //autoRunEntry = Helper.GetFilePathWithNoParameters(dms, autoRunEntry, @"C:\windows\system32\cmd.exe /D /C start C:\windows\system32\ie4uinit.exe -ClearIconCache");

             //AutoRunEntry autoRunEntry = new AutoRunEntry();
             //autoRunEntry = Helper.GetFilePathWithNoParameters(dms, autoRunEntry, @"c:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\PrivateAssemblies\Microsoft.VisualStudio.QualityTools.RecorderBarBHO100.dll");
             

            //Console.WriteLine(autoRunEntry.FilePath);

            //return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
