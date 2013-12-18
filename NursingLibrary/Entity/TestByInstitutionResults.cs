using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class TestByInstitutionResults
    {
        public decimal Percantage { get; set; }

        public Cohort Cohort { get; set; }
        
        public Institution Institution { get; set; }
        
        public int NStudents { get; set; }

        public string Normed { get; internal set; }
    }
}
