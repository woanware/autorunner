using System.ComponentModel;

namespace autorunner
{
    /// <summary>
    /// Global class for storing global stuff
    /// </summary>
    public class Global
    {
        /// <summary>
        /// Enumeration for the various different autorun entries supported, 
        /// allows for different parsing depending on the particular type
        /// </summary>
        public enum AutoRunType
        {
            RegistryValue = 0,
            RegistryStubValue = 1,
            FolderDefault = 2,
            FolderUsers = 3,
            Service = 4,
            Driver = 5,
            AppInit = 6,
            Bho = 7
        }

        /// <summary>
        /// Enumeration for the various different Windows registry hives 
        /// that are required to be parsed for a particular entry
        /// </summary>
        public enum Hive
        {
            [Description("Not Applicable")]
            NotApplicable = 0,
            [Description("SYSTEM")]
            System = 1,
            [Description("SOFTWARE")]
            Software = 2,
            [Description("NTUSER.DAT")]
            Ntuser = 3,
            [Description("USRCLASS.DAT")]
            Usrclass = 5
        }

        /// <summary>
        /// </summary>
        public enum Columns
        {
            Type = 0,
            Path = 1,
            FilePath = 2,
            FileName = 3,
            Parameters = 4,
            Publisher = 5,
            FileDate = 6,
            SigningDate = 7,
            Version = 8,
            Md5 = 9,
            Description = 10,
            Info = 11
        }

        public const string SIGCHECK_FLAGS = " -accepteula -a -q ";
    }
}
