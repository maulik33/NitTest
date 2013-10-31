using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentCaseStudiesView
    {
        void AddCaseTable(IEnumerable<CaseStudy> caseStudies);
    }
}
