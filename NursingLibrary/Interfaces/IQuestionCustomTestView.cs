using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IQuestionCustomTestView
    {
        int TestId { get; set; }

        int ProductId { get; set; }

        int ProgramofStudyId { get; set; }

        void RenderQuestionCustomTest(Test test, IEnumerable<QuestionResult> questionResult);

        void PopulateSearchCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categoryData);
        
        void PopulateClientNeedsCategory(IDictionary<int, CategoryDetail> clientNeedsCategories);
        
        void PopulateTests(IEnumerable<Test> tests);

        void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> clientNeedsCategories);

        void PopulateProgramofStudyParameters(IEnumerable<Product> products, IEnumerable<Topic> topics);
    }
}
