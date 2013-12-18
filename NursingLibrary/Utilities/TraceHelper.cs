using System.Collections.Generic;
using log4net;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    public class TraceHelper
    {
        private static ILog log = LogManager.GetLogger(typeof(TraceHelper));
        private TraceInfo _traceInfo;

        public TraceHelper(TraceInfo traceInfo)
        {
            _traceInfo = traceInfo;
            _traceInfo.Data = new List<KeyValuePair<string, string>>();
        }

        public static void WriteTraceEvent(TraceToken token, string message)
        {
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.Event,
                    Id = token.Id,
                    Message = message
                };
                WriteTrace(traceMessage);
            }
            catch
            {
            }
        }

        public static void WriteTraceEvent(TraceToken token, string message, params string[] msgPlaceHolderValues)
        {
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.Event,
                    Id = token.Id,
                    Message = string.Format(message, msgPlaceHolderValues)
                };
                WriteTrace(traceMessage);
            }
            catch
            {
            }
        }

        public static void WriteTraceEvent(TraceToken token, string message, IList<KeyValuePair<string, string>> data)
        {
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.Event,
                    Id = token.Id,
                    Message = message,
                    Data = data
                };
                WriteTrace(traceMessage);
            }
            catch
            {
            }
        }

        public static void WriteTraceError(TraceToken token, string message)
        {
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.Error,
                    Id = token.Id,
                    Message = message
                };
                WriteTrace(traceMessage);
            }
            catch
            {
            }
        }

        public static void WriteTraceEvent(TraceToken token, string message, IList<KeyValuePair<string, string>> data,
            params string[] msgPlaceHolderValues)
        {
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.Event,
                    Id = token.Id,
                    Message = string.Format(message, msgPlaceHolderValues),
                    Data = data
                };
                WriteTrace(traceMessage);
            }
            catch
            {
            }
        }

        public static TraceToken BeginTrace(int userId, string userName, string environment)
        {
            TraceToken token = new TraceToken();
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.Init,
                    Id = token.Id,
                    Message = "Trace Begin",
                    Environment = environment,
                    UserId = userId,
                    UserName = userName
                };
                log.Info(traceMessage.ToString());
            }
            catch
            {
            }
            
            return token;
        }

        public static void WriteTraceEnd(TraceToken token)
        {
            try
            {
                TraceInfo traceMessage = new TraceInfo()
                {
                    Type = TraceType.End,
                    Id = token.Id,
                    Message = "Trace End"
                };
                log.Info(traceMessage.ToString());
            }
            catch
            {
            }
        }

        public static TraceHelper Create(TraceToken token, string message)
        {
            TraceInfo traceMessage = new TraceInfo()
            {
                Type = TraceType.Event,
                Id = token.Id,
                Message = message
            };
            return new TraceHelper(traceMessage);
        }

        public static TraceHelper Create(TraceToken token, string message, params string[] msgPlaceHolderValues)
        {
            TraceInfo traceMessage = new TraceInfo()
            {
                Type = TraceType.Event,
                Id = token.Id,
                Message = string.Format(message, msgPlaceHolderValues)
            };
            return new TraceHelper(traceMessage);
        }

        public TraceHelper Add(string variableName, string value)
        {
            try
            {
                _traceInfo.Data.Add(new KeyValuePair<string, string>(variableName, value.Replace('|', ',')));
            }
            catch
            {
            }

            return this;
        }

        public void Write()
        {
            try
            {
                WriteTrace(_traceInfo);
            }
            catch
            {
            }
        }

        private static void WriteTrace(TraceInfo traceMessage)
        {
            if (KTPApp.TraceEnabled)
            {
                log.Info(traceMessage.ToString());
            }
        }
    }
}
