using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class QuestionCriteria
    {
        public int ProgramOfStudy { get; set; }

        public int Product { get; set; }

        public int Test { get; set; }
        
        public int ClientNeed { get; set; }
        
        public int ClientNeedsCategory { get; set; }
        
        public int NursingProcess { get; set; }
        
        public int LevelOfDifficulty { get; set; }
        
        public int Demographic { get; set; }
        
        public int CognitiveLevel { get; set; }
        
        public int SpecialtyArea { get; set; }
        
        public int System { get; set; }

        public int CriticalThinking { get; set; }
        
        public int ClinicalConcept { get; set; }
        
        public int Remediation { get; set; }
        
        public string QuestionID { get; set; }
        
        public string Text { get; set; }
        
        public string Qtype { get; set; }
        
        public string ItemType { get; set; }
        
        public bool GetChanges { get; set; }
        
        public int ReleaseStatus { get; set; }
        
        public int Active { get; set; }

        public int AccreditationCategories { get; set; }

        public int QSENKSACompetencies { get; set; }

        public int Concepts { get; set; }
    }
}
