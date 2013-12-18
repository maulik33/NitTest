using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    // Sudhin added this // -- the parameters are hardcoded as understood from the UI -- not sure of the functionality
    public interface IQBankPView
    {
        void BindData(IEnumerable<ProgramResults> perfOverViewList, IEnumerable<ProgramResults> perfListWith4, IEnumerable<ProgramResults> perfListWith5);
    }
}
