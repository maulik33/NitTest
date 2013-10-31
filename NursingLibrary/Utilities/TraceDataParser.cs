using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    public class TraceDataParser
    {
        public static TraceInfo ToObject(string data)
        {
            return CreateByType(data, null);
        }

        public static bool IsErrorData(string data)
        {
            return data.Trim().StartsWith("at");
        }

        public static string ToString(TraceInfo traceEntity)
        {
            string toString = string.Empty;
            switch (traceEntity.Type)
            {
                case TraceType.Init:
                    toString = string.Format("{0}|{1}|{2}|{3}|{4}", traceEntity.Id,
                        (int)traceEntity.Type, traceEntity.UserId, traceEntity.UserName, traceEntity.Environment);
                    break;
                default:
                    toString = string.Format("{0}|{1}|{2}", traceEntity.Id, (int)traceEntity.Type, traceEntity.Message);
                    break;
            }

            return GetDataString(traceEntity, toString);
        }

        public static TraceInfo CreateByType(string data, TraceType? type)
        {
            if (data.Length < 45)
            {
                return null;
            }

            string[] parts = data.Substring(13).Split('|');
            if (parts.Length < 2 || parts.Length > 10)
            {
                return null;
            }

            TraceType currentType = (TraceType)Enum.Parse(typeof(TraceType), parts[1]);
            if (type != null
                && type != currentType)
            {
                return null;
            }

            TraceInfo entity = new TraceInfo()
            {
                Id = new Guid(parts[0]),
                Type = currentType,
                Time = data.Substring(0, 12),
                Data = new List<KeyValuePair<string, string>>()
            };
            Populate(entity, parts);
            return entity;
        }

        private static string GetDataString(TraceInfo traceEntity, string message)
        {
            if (traceEntity.Data == null
                || traceEntity.Data.Count() == 0)
            {
                return message;
            }

            return string.Format("{0}|{1}", message,
                String.Join("$", traceEntity.Data.Select(p => string.Format("{0} = {1}", p.Key, p.Value)).ToArray()));
        }

        private static void SetDataString(TraceInfo traceEntity, string dataString)
        {
            string[] parts = dataString.Split('$');
            foreach (var item in parts)
            {
                string[] value = item.Split('=');
                traceEntity.Data.Add(new KeyValuePair<string, string>(value[0], value[1]));
            }
        }

        private static void Populate(TraceInfo entity, string[] parts)
        {
            switch (entity.Type)
            {
                case TraceType.Init:
                    {
                        entity.UserId = parts[2].ToInt();
                        entity.Environment = parts[3];
                        if (parts.Length > 4)
                        {
                            entity.UserName = parts[3];
                            entity.Environment = parts[4];
                        }
                        
                        break;
                    }

                case TraceType.Event:
                    {
                        entity.Message = parts[2];
                        if (parts.Length > 3)
                        {
                            SetDataString(entity, parts[3]);
                        }
                        
                        break;
                    }

                default:
                    {
                        entity.Message = parts[2];
                        break;
                    }
            }
        }
    }
}
