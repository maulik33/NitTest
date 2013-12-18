using System.Data;

namespace NursingLibrary.Interfaces
{
    public interface IReportTestResultByQuestionView : IReportView
    {
        bool IsInstitutionIdExistInQueryString { get; set; }

        bool IsTestTypeIdExistInQueryString { get; set; }
        
        bool IsTestIdExistInQueryString { get; set; }
        
        bool IsCohortIdExistInQueryString { get; set; }
        
        bool IsRTypeExistInQueryString { get; set; }
        
        bool IsModeExistInQueryString { get; set; }

        bool IsProgramofStudyExistInQueryString { get; set; }

        bool IsSelectedCohorts { get; set; }
        
        int CohortStartIndex { get; set; }
        
        int CohortEndIndex { get; set; }

        int CohortNumberOfAPage { get; }

        string Mode { get; set; }
        
        string RType { get; set; }
        
        void HideButton();

        void RenderReport(DataTable reportData);
        
        void ExportReport(DataTable reportData, ReportAction printActions);

        bool IsProgramofStudyVisible { get; set; }
    }
}
