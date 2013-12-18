using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportView
    {
        void PopulateInstitutions(IEnumerable<Institution> institutions);
        
        void PopulateProducts(IEnumerable<Product> products);
        
        void PopulateCohorts(IEnumerable<Cohort> cohorts);
        
        void PopulateTests(IEnumerable<UserTest> tests);
        
        void GenerateReport();

        void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies);

        bool PostBack { get; }
    }
}
