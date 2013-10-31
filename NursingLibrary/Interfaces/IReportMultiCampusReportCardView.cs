using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportMultiCampusReportCardView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void PopulateGroup(IEnumerable<Group> groups);

        void PopulateStudent(IEnumerable<StudentEntity> students);

        void RenderReport(IEnumerable<MultiCampusReportDetails> reportData);

        void GenerateReport(IEnumerable<MultiCampusReportDetails> reportData, ReportAction printActions);
    }
}
