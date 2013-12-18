using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentQBankRDetailsView
    {
        IEnumerable<ProgramResults> ProgramResults { set; }

        int UserTestID { get; }

        void LoadTables_N(IEnumerable<Category> list_C);
        
        void BindData(ProgramResults programResult);
    }
}
