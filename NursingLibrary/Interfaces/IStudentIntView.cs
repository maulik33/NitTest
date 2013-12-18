using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentIntView
    {
        void LoadProducts(IEnumerable<Product> products);

        void LoadTests(IEnumerable<UserTest> tests);
        
        void UpdateReviewResultsHeading();
        
        void ShowTestResults(IEnumerable<UserQuestion> testQuestions, IEnumerable<Category> testCharacteristics, TestType testType, bool isTestId74);
    }
}
