using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    /// <summary>
    /// Represents IReportCohortByTestView interface
    /// </summary>
    public interface IReportCohortByTestView : IReportView
    {
        string Mode { get; }

        bool IsProgramofStudyVisible { get; set; }

        void PopulateGroup(IEnumerable<Group> groups);

        void RenderReport(IEnumerable<CohortByTest> reportData);

        void GenerateReport(IEnumerable<CohortByTest> reportData, ReportAction printActions);
    }
}
