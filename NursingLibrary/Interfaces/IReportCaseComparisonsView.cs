using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportCaseComparisonsView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        int Act { get; set; }

        void PopulateCase(IEnumerable<CaseStudy> caseStudies);

        void PopulateModule(IEnumerable<Modules> modules);
        
        void PopulateCategories(IEnumerable<Category> categories);
        
        void PopulateSubCategories(IDictionary<string, string> categoryDetails);
        
        void RenderReport();
        
        void ExportReport(ReportAction printActions);
    }
}
