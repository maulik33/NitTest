using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class SaveInstitutionContactEventArgs : EventArgs
    {
        public InstitutionContact InstitutionContactInfo { get; set; }
    }
}
