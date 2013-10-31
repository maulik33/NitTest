using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportLaunchDxrView
    {
        string EnrolmentId
        {
            get;
            set;
        }

        void GetCaseDetails(string contentId, string firstName, string lastName);
    }
}
