namespace autorunner
{
    /// <summary>
    /// Encapsulates one entry in the Configuration XML file
    /// </summary>
    public class Config
    {
        #region Member Variables
        public string Path { get; set; }
        public autorunner.Global.AutoRunType EntryType { get; set; }
        public autorunner.Global.Hive Hive { get; set; }
        public string Type { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Config()
        {
            Path = string.Empty;
            Type = string.Empty;
        }
        #endregion
    }
}
