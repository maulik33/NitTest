using System;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class UserAuthentication
    {
        public Admin User { get; set; }

        public bool IsAdminLogin { get; set; }
        
        public AuthenticationRequest Status { get; set; }
    }
}
