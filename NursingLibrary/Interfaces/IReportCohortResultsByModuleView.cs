using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportCohortResultsByModuleView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        bool IsInstitutionIdExistInQueryString { get; set; }

        bool IsCaseIdExistInQueryString { get; set; }
        
        bool IsModuleIdExistInQueryString { get; set; }
        
        bool IsCohortIdExistInQueryString { get; set; }

        bool IsProgramOfStudyIdExistInQueryString { get; set; }
        
        void PopulateCases(IEnumerable<CaseStudy> cases);
        
        void PopulateModule(IEnumerable<Modules> module);

        void RenderReport(CohortResultsByModule cohortResultsByModule, IEnumerable<CategoryDetail> subCategories);
        
        void GenerateReport(CohortResultsByModule cohortResultsByModule, IEnumerable<CategoryDetail> subCategories, ReportAction printActions);
    }
}
