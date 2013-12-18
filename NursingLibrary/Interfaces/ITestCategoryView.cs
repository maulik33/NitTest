using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ITestCategoryView
    {
        int TestId { get; set; }

        int TestType { get; set; }

        int ProgramOfStudyId { get; set; }

        void RenderTestCategories(IEnumerable<ProgramofStudy> programOfStudies, IEnumerable<Product> products, IEnumerable<Test> tests);
    }
}
