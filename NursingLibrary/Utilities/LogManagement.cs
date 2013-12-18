using System;
using log4net;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    public class LogManagement : ILogManagement
    {
        private ILog _log;

        private ILog Log
        {
            get
            {
                if (_log == null)
                {
                    SetLogger(GetType().Name);
                }

                return _log;
            }
        }

        public void SetLogger(string logger)
        {
            _log = LogManager.GetLogger(logger);
        }

        #region LogInfo
        public void LogInfo(string message)
        {
            LogInfo(message, null);
        }

        public void LogInfo(string message, Exception ex)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message, ex);
            }
        }
        #endregion

        #region LogWarn
        public void LogWarning(string message)
        {
            LogWarning(message, null);
        }

        public void LogWarning(string message, Exception ex)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message, ex);
            }
        }
        #endregion

        #region LogDebug
        public void LogDebug(string message)
        {
            LogDebug(message, null);
        }

        public void LogDebug(string message, Exception ex)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message, ex);
            }
        }
        #endregion

        #region LogError
        public void LogError(string message)
        {
            LogError(message, null);
        }

        public void LogError(string message, Exception ex)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(message, ex);
            }
        }
        #endregion

        #region LogFatal
        public void LogFatal(string message)
        {
            LogFatal(message, null);
        }

        public void LogFatal(string message, Exception ex)
        {
            if (Log.IsFatalEnabled)
            {
                Log.Fatal(message, ex);
            }
        }
        #endregion
    }
}
