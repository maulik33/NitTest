using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class ReportTestsScheduledbyDate
    {
        public string InstitutionName { get; set; } 

        public string CohortName { get; set; }

        public string TestType { get; set; }

        public string TestName { get; set; }

        public DateTime? StartDate { get; set; }

        public int NumberOfStudents { get; set; }
    }
}
