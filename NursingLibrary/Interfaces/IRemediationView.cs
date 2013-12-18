using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IRemediationView
    {
        string DisplayMessage { set; }

        void PopulateControls(Remediation remediation);

        void PopulateQuestions(IEnumerable<Question> questions);

        void PopulateLippincotts();

        void DisplayUploadedTopics(IEnumerable<Remediation> validRemediations, IEnumerable<Remediation> invalidRemediations, IEnumerable<Remediation> duplicateTopics, string filePath);
    }
}
