using System;

namespace autorunner
{
    /// <summary>
    /// Encapsulates one auto run entry
    /// </summary>
    public class AutoRunEntry
    {
        #region Member Variables
        public string Type { get; set; }
        public string Path { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Parameters { get; set; }
        public bool Exists { get; set; }
        public string Verified { get; set; }
        public string StrongName { get; set; }
        public string FilePublisher { get; set; }
        public DateTime FileDate { get; set; }
        public DateTime SigningDate { get; set; }
        public string Version { get; set; }
        public string FileVersion { get; set; }
        public string FileDescription { get; set; }
        public string Info { get; set; }
        public string Guid { get; private set; }
        public string Md5 { get; set; }
        public string Sha256 { get; set; }
        public DateTime FileSystemCreated { get; set; }
        public DateTime FileSystemModified { get; set; }
        public DateTime FileSystemAccessed { get; set; }
        public DateTimeOffset? RegistryModified { get; set; }
        public string SourceFile { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceDescription { get; set; }
        public string InternalName { get; set; }
        public string Error { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public AutoRunEntry()
        {
            Type = string.Empty;
            Path = string.Empty;
            FilePath = string.Empty;
            FileName = string.Empty;
            Parameters = string.Empty;
            Verified = string.Empty;
            StrongName = string.Empty;
            Version = string.Empty;
            FileVersion = string.Empty;
            FileDescription = string.Empty;
            FilePublisher = string.Empty;
            Info = string.Empty;
            Md5 = string.Empty;
            SourceFile = string.Empty;
            ServiceDisplayName = string.Empty;
            ServiceDescription = string.Empty;
            Error = string.Empty;

            FileDate = DateTime.MinValue;
            SigningDate = DateTime.MinValue;
            FileSystemCreated = DateTime.MinValue;
            FileSystemModified = DateTime.MinValue;
            FileSystemAccessed = DateTime.MinValue;
            RegistryModified = new DateTimeOffset();

            Guid = System.Guid.NewGuid().ToString(); // Generate a custom guid/key for the image list icon stuff
        }
        #endregion

        #region Properties
        /// <summary>
        /// Added to improve the display of dates in the ObjectListView e.g. they will be a default value if not set from a binary or sigcheck value
        /// </summary>
        public string FileDateText
        {
            get
            {
                if (FileDate == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return FileDate.ToString("s");
            }
        }

        /// <summary>
        /// Added to improve the display of dates in the ObjectListView e.g. they will be a default value if not set from a binary or sigcheck value
        /// </summary>
        public string SigningDateText
        {
            get
            {
                if (SigningDate == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return SigningDate.ToString("s");
            }
        }

        /// <summary>
        /// Added to improve the display of dates in the ObjectListView e.g. they will be a default value if not set from a binary or sigcheck value
        /// </summary>
        public string FileSystemCreatedDateText
        {
            get
            {
                if (FileSystemCreated == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return FileSystemCreated.ToString("s");
            }
        }

        /// <summary>
        /// Added to improve the display of dates in the ObjectListView e.g. they will be a default value if not set from a binary or sigcheck value
        /// </summary>
        public string FileSystemModifiedDateText
        {
            get
            {
                if (FileSystemModified == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return FileSystemModified.ToString("s");
            }
        }

        /// <summary>
        /// Added to improve the display of dates in the ObjectListView e.g. they will be a default value if not set from a binary or sigcheck value
        /// </summary>
        public string FileSystemAccessedDateText
        {
            get
            {
                if (FileSystemAccessed == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return FileSystemAccessed.ToString("s");
            }
        }

        /// <summary>
        /// Added to improve the display of dates in the ObjectListView e.g. they will be a default value if not set from a binary or sigcheck value
        /// </summary>
        public string RegistryModifiedText
        {
            get
            {
                return RegistryModified != null ? RegistryModified.Value.ToString("s") : ""; //RegistryModified.ToString("s"); 
            }
        }
        #endregion
    }
}
