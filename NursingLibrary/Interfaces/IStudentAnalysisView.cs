using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentAnalysisView
    {
        string NumberCorrect { get; }

        IEnumerable<ProgramResults> ProgramResults { set; }

        void PopulateProducts(IEnumerable<Product> products);
        
        void PopulateTestsByUser(IEnumerable<UserTest> userTests);
        
        void BindData(ProgramResults programResult);
        
        void LoadTables_I(IEnumerable<Category> list_C, int probability, int percentileRank, bool probabilityExists,
                          bool percentileRankExists);
    }
}
