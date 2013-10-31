using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class AdhocGroupTestDetails
    {
        public int AdhocGroupTestDetailId { get; set; }

        public int TestId { get; set; }

        public int AdhocGroupId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public Test Test { get; set; }

        public int Type { get; set; }
    }
}
