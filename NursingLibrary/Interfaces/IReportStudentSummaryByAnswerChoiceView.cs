using System.Data;

namespace NursingLibrary.Interfaces
{
    public interface IReportStudentSummaryByAnswerChoiceView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void RenderReport(DataTable reportData);
        
        void ExportReport(DataTable reportData);
    }
}
