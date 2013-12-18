using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Program
    {
        public Cohort Cohort { get; set; }

        public int ProgramId { get; set; }
        
        public string ProgramName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime? DeletedDate { get; set; }

        public bool IsTestAssignedToProgram { get; set; }

        public int ProgramOfStudyId { get; set; }
        
        public string ProgramOfStudyName { get; internal set; }

        public int ReferenceProgramId { get; set; }
    }
}
