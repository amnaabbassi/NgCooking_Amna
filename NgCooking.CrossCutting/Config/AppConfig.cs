using System;
using System.Configuration;
using System.Linq;

namespace NgCooking.CrossCutting.Config
{
    class AppConfig
    {
        public class Keys
        {
            public const string CURRENT_ENVIRONMENT = "CURRENT_ENVIRONMENT";
            public const string DB_CONNECTION_NGCOOKING = "DB_CONNECTION_NGCOOKING";
        }

        #region Attributes and Properties
        
       
        public static string DbConnectionStringNgCooking
        {
            get
            {
                return ReadSetting(Keys.DB_CONNECTION_NGCOOKING);
            }
        }

        public static string GetConnexionstring(string name)
        {

            string StrConn = string.Empty;
            try
            {
                ConnectionStringSettings settings =
             ConfigurationManager.ConnectionStrings[name];

                if (settings != null)
                {
                    StrConn = settings.ConnectionString;
                }
            }
            catch { }
            return StrConn;

        }

        public static string CurrentEnvironment
        {
            get
            {
                return  ConfigurationManager.AppSettings[Keys.CURRENT_ENVIRONMENT];
            }
        }
     
       
        public static string ReadSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            string keyWithEnvironment = string.Format("{0}_{1}", key, CurrentEnvironment);
           
            
            if (AppSettingsKeys.Contains(keyWithEnvironment))
            {
                // Variable de configuration associée à l'environnement :
                return ConfigurationManager.AppSettings[keyWithEnvironment];
            }
            else if (AppSettingsKeys.Contains(key))
            {
                // Variable de configuration indépendante de l'environnement :
                return ConfigurationManager.AppSettings[key];
            }
            else
            {
                // Variable de configuration introuvable avec ou sans le suffixe d'environnement :
                throw new Exception(string.Format("Clé de configuration introuvable : \"{0}\".", key));
            }
        }
        //-------------------------------
        private static string[] _AppSettingsKeys;
        private static ConnectionStringSettingsCollection _ConnectionStringKeys;
        protected static string[] AppSettingsKeys
        {
            get
            {
                if (_AppSettingsKeys == null)
                    _AppSettingsKeys = ConfigurationManager.AppSettings.Keys.Cast<string>().ToArray();

                return _AppSettingsKeys;
            }
        }
        protected static ConnectionStringSettingsCollection ConnectionStringKeys
        {
            get
            {
                if (_ConnectionStringKeys == null)
                    _ConnectionStringKeys = ConfigurationManager.ConnectionStrings;

                return _ConnectionStringKeys;
            }
        }
        #endregion
    }
}
