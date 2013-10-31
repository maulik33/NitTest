using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class CaseModuleScore
    {
        public int ModuleStudentId { get; set; }

        public int CaseId { get; set; }
        
        public int ModuleId { get; set; }
        
        public string StudentId { get; set; }
        
        public int Correct { get; set; }
        
        public int Total { get; set; }
    }
}
