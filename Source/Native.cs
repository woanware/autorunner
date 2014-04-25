using System;
using System.Runtime.InteropServices;
using System.Text;

namespace autorunner
{
    /// <summary>
    /// 
    /// </summary>
    public class Native
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetLongPathName([MarshalAs(UnmanagedType.LPTStr)]string path, [MarshalAs(UnmanagedType.LPTStr)]StringBuilder longPath, int longPathLength);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]string path, [MarshalAs(UnmanagedType.LPTStr)]StringBuilder shortPath, int shortPathLength);
    }
}
