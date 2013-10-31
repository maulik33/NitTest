using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IPercentileView
    {
        int TestID { get; set; }
        
        int NormingID { get; set; }

        int TestType { get; set; }

        int ProgramOfStudyId { get; set; }

        List<Norming> NormingDetails { get; set; }

        Norming NormingProp { get; set; }
        
        void RefreshNormingDetails();

        void PopulateProducts(IEnumerable<ProgramofStudy> programofStudies, IEnumerable<Product> products,
                              IEnumerable<Test> tests);
    }
}
