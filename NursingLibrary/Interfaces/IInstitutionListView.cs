using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IInstitutionListView 
    {
        void ShowInstitutionResults(IEnumerable<NurInstitution> Institutions,string orderByProperty,string orderDirection);

        
    }
}
