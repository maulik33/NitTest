using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class EnglishForNursingTracking
    {
        public string InstitutionName { get; set; }

        public StudentEntity Student { get; set; }

        public string CohortName { get; set; }       

        public string TestName { get; set; }

        public DateTime? AltTabClickedDate { get; set; }

        public string RemediationTime { get; set; }       

        public string TimeUsed { get; set; }

        public string QuestionId { get; set; }

        public int TestId { get; set; }

        public string Correct { get; set; }

        public string UserAction { get; set; }
    }
}
