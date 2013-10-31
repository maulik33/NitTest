using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportStudentReportCardByModuleView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        string CaseName { get; set; }
        
        void PopulateStudent(IEnumerable<StudentEntity> students);
        
        void PopulateCases(IEnumerable<CaseStudy> cases);
        
        void PopulateModule(IEnumerable<Modules> module);

        void RenderReport(IEnumerable<ResultsForStudentReportCardByModule> reportData);
        
        void GenerateReport(IEnumerable<ResultsForStudentReportCardByModule> reportData, ReportAction printActions);
    }
}
