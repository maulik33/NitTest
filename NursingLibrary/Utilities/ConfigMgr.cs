using System;
using System.Configuration;

namespace NursingLibrary.Utilities
{
    public static class ConfigMgr
    {
        public static string GetConfigValue(string index)
        {
            return !string.IsNullOrEmpty(index) ? CacheMgr.Get<string>(index, LoadConfigValue, TimeSpan.FromHours(24)) : null;
        }

        public static string GetConnectionStringValue(string index)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[index].ToString();
            }
            catch (Exception ex)
            {
                Logger.LogError("GetConnectionStringValue Error", ex);
                Logger.LogDebug(ex.StackTrace);
            }
            return null;
        }

        private static string LoadConfigValue(object cacheKey)
        {
            return ConfigurationManager.AppSettings.Get(cacheKey.ToString());
        }
    }
}
