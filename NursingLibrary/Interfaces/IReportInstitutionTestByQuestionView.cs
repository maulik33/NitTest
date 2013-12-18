using System.Data;

namespace NursingLibrary.Interfaces
{
    public interface IReportInstitutionTestByQuestionView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        bool IsSelectedCohorts { get; set; }

        int CohortStartIndex { get; set; }

        int CohortEndIndex { get; set; }

        int CohortNumberOfAPage { get; }

        void HideButton();

        void RenderReport(DataTable reportData);

        void ExportReport(DataTable reportData, ReportAction printActions);
    }
}
