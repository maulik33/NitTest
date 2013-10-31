using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public interface ISysAdminView
    {
        void DisplayCheckSystemResults(IDictionary<int, string> results);
    }
}
