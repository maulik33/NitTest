using System;
using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportTestsScheduledbyDateView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }

        string SelectedProgramOfStudyName { get; }

        bool IsAllCohortsSelected { get; set; }

        bool IsAllGroupSelected { get; set; }

        void PopulateGroup(IEnumerable<Group> groups);

        void RenderReport(IEnumerable<ReportTestsScheduledbyDate> reportData);

        void GenerateReport(IEnumerable<ReportTestsScheduledbyDate> reportData, ReportAction printActions);
    }
}
