using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportStudentReportCardView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void PopulateGroup(IEnumerable<Group> groups);

        void PopulateStudent(IEnumerable<StudentEntity> students);

        void RenderReport(IEnumerable<StudentReportCardDetails> reportData);

        void GenerateReport(IEnumerable<StudentReportCardDetails> reportData, ReportAction printActions);
    }
}
