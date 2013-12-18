using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class QuestionResult
    {
        public int QID { get; set; }

        public int QN { get; set; }
        
        public string QuestionID { get; set; }
        
        public string TopicTitle { get; set; }
        
        public string System { get; set; }
        
        public string NursingProcess { get; set; }
        
        public string ClientNeeds { get; set; }
        
        public string ClientNeedCategory { get; set; }
        
        public string ReleaseStatus { get; set; }
        
        public string Stem { get; set; }
        
        public int Scramble { get; set; }

        public string LevelofDifficulty { get; set; }

        public string ClinicalConcept { get; set; }

        public string Demographic { get; set; }        
    } 
}
