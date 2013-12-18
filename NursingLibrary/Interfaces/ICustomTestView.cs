using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ICustomTestView
    {
        int TestId { get; set; }

        int PageIndex { get; set; }

        string SearchCondition { get; set; }

        string Sort { get; set; }

        void RenderCustomTest(IEnumerable<ProgramofStudy> ProgramofStudies, IEnumerable<Product> products, Test test);
        
        void DisplaySearchResult(IEnumerable<Test> tests, SortInfo sortMetaData);
    }
}
