using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReleaseView
    {
        string ShowContent { get; set; }

        string showLippincot { get; set; }
        
        string showTests { get; set; }
        
        void RenderReviewDetails(IEnumerable<Question> questions, List<Remediation> remediations, IEnumerable<Lippincott> lippincotts, List<Test> tests);
    }
}
