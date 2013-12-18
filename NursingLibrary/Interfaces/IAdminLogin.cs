using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IAdminLogin
    {
        void ShowMessage(UserAuthentication result); 
    }
}
