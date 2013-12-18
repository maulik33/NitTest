using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class Remediation
    {
        public int RemediationId { get; set; }

        public string Explanation { get; set; }
        
        public string TopicTitle { get; set; }
        
        public string ReleaseStatus { get; set; }

        public string ErrorMessage { get; set; }
    }
}
