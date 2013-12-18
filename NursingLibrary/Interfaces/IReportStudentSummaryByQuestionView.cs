using System.Data;

namespace NursingLibrary.Interfaces
{
    public interface IStudentSummaryReportByQuestionView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void RenderReport(DataTable reportData);
        
        void ExportToExcel(DataTable reportData);
    }
}
