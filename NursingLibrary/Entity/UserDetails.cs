using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
   public class UserDetails
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserPass { get; set; }

        public string UserPassEncrypted { get; set; }

        public string InstitutionName { get; set; }
        
    }
}
