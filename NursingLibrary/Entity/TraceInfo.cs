using System;
using System.Collections.Generic;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Entity
{
    public class TraceInfo
    {
        private string _userName;

        public TraceInfo()
        {
        }

        public TraceType Type { get; set; }

        public string Environment { get; set; }

        public string Message { get; set; }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value.ToLower();
            }
        }

        public int UserId { get; set; }

        public Guid Id { get; set; }

        public string Time { get; set; }

        public bool HasError { get; set; }

        public IList<KeyValuePair<string, string>> Data { get; set; }

        public override string ToString()
        {
            return TraceDataParser.ToString(this);
        }
    }
}
