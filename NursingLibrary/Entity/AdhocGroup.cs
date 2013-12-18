using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class AdhocGroup
    {
        public int AdhocGroupId { get; set; }

        public string AdhocGroupName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool IsAdaGroup { get; set; }

        public bool? ADA { get; set; }
    }
}
