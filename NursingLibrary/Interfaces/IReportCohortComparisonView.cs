using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportCohortComparisonView : IReportView
    {
        int Act { get; set; }

        void PopulateCategories(IEnumerable<Category> categories);

        void PopulateSubCategories(IEnumerable<CategoryDetail> categoryDetails);
        
        void RenderReport();
        
        void GenerateReport(ReportAction printActions);

        void PopulateProgramOfStudiesForTestsAndCategories(IEnumerable<ProgramofStudy> programOfStudies, int? selectedProgramOfStudy = null);
    }
}
