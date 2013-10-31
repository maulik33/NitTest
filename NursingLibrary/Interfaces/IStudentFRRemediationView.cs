using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentFRRemediationView
    {
        string SystemId { get; }

        string TopicId { get; }

        string CategoryIds { get; set; }

        int NumberOfRemediations { get; set; }

        string ReviewRemName { get; set; }

        int RemTime { get; set; }

        int Timer { get; }

        string RemediationNumber { get; }

        string TestName { set; }

        string SystemName { get; set; }

        void PopulateSystems(IEnumerable<Systems> systems);

        void PopulateTopics(IEnumerable<Topic> topics);

        void SetModeDetails();

        void SetRemediationCtrl(int remID, string action);

        void HideShowPreviousIncorrectButton(bool p);

        void PopulateEndForAllPages();

        void PopulateRemediation(ReviewRemediation rem);

        ////To remove the commented line once Gokul confirms the UI without Lippincott
        ////void ShowLippincott(IEnumerable<Lippincott> lippinCotts, string explaination);

        void BindRemediationList(IEnumerable<ReviewRemediation> getTestsForTheUser, SortInfo sortMetaData);

        void PopulateFields(ReviewRemediation ReviewRemediation);

        void PopulateEnd(ReviewRemediation remediation, bool lastRemediation);
    }
}
