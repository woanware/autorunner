using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using woanware;

namespace autorunner
{
    /// <summary>
    /// Allows us to save/load the configuration file to/from XML
    /// </summary>
    public class Settings
    {
        #region Member Variables
        public string ApiKey { get; set; }
        private const string FILENAME = "Settings.xml";
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Settings()
        {
            ApiKey = string.Empty;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load()
        {
            try
            {
                string path = GetPath();

                if (File.Exists(path) == false)
                {
                    return string.Empty;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    Settings settings = (Settings)serializer.Deserialize(stream);
                    ApiKey = settings.ApiKey;
                    return string.Empty;
                }
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (StreamWriter writer = new StreamWriter(GetPath(), false))
                {
                    serializer.Serialize((TextWriter)writer, this);
                    return string.Empty;
                }
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            return System.IO.Path.Combine(Misc.GetApplicationDirectory(), FILENAME);
        }
        #endregion
    }
}
