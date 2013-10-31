using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentListOfReviewsView
    {
        string EndQuery { get; }

        void PopulateProducts(IEnumerable<Product> products);

        void BindFinishedTest(IEnumerable<FinishedTest> finishedTests);

        void SetTestResultinks();
    }
}
