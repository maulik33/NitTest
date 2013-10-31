using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface INormView
    {
        string Category { get; }

        int TestId { get; }

        int ProgramodStudy { get; }

        void PopulateNorm(IDictionary<int, CategoryDetail> listOfNormEnity, IEnumerable<Norm> overallNormValues, IEnumerable<Norm> specificNormValues);

        void PopulateControls(IEnumerable<Product> products, IEnumerable<ProgramofStudy> programofStudy);

        void PopulateTest(IEnumerable<Test> tests);

        void PopulateCategories(IDictionary<CategoryName, Entity.Category> categories);
    }
}
