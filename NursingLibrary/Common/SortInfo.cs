using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Common
{
    public class SortInfo
    {
        public string SortExpression { get; set; }

        public SortOrder Direction { get; set; }

        public override string ToString()
        {
            return string.Format("{0}|{1}", SortExpression, SortHelper.Parse(Direction));
        }
    }
}
