namespace autorunner
{
    /// <summary>
    /// 
    /// </summary>
    public class DriveMapping
    {
        #region Member Variables\Properties
        public string OriginalDrive { get; set; }
        public string MappedDrive { get; set; }
        public bool IsWindowsDrive { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public DriveMapping()
        {
            OriginalDrive = string.Empty;
            MappedDrive = string.Empty;
        }
        #endregion
    }
}
