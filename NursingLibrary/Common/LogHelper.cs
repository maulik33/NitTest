using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Common
{
    public class LogHelper
    {
        private readonly DateTime OFFSET_DATE = new DateTime(2011, 4, 4);
        private static LogHelper _instance;
        private static object _lockObject = new object();
        private long _errorId = 0;

        public string Prefix { get; set; }

        private static LogHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        public static void Log()
        {
        }

        public static void Initialize(string machineName)
        {
            CreateInstance();
            switch (machineName)
            {
                case "NITPNYC1W1":
                    Instance.Prefix = "1";
                    break;
                case "NITPNYC1W2":
                    Instance.Prefix = "2";
                    break;
                case "NITPNYC1W3":
                    Instance.Prefix = "3";
                    break;
                case "NITPNYC1W4":
                    Instance.Prefix = "4";
                    break;
                default:
                    Instance.Prefix = "RN";
                    break;
            }

            Instance.Prefix = string.Format("{0}.{1}", Instance.Prefix, DateTime.UtcNow.Subtract(Instance.OFFSET_DATE).Days);
        }

        public static LogInfo CreateLog(string message)
        {
            LogInfo logMessage = new LogInfo();
            logMessage.ErrorCode = Instance.GetErrorCode();
            logMessage.Message = string.Format("{0}|{1}", message, logMessage.ErrorCode);
            return logMessage;
        }

        private static void CreateInstance()
        {
            _instance = new LogHelper();
        }

        private string GetErrorCode()
        {
            lock (_lockObject)
            {
                _errorId++;
            }

            return string.Format("{0}.{1}", Instance.Prefix, _errorId);
        }
    }
}
