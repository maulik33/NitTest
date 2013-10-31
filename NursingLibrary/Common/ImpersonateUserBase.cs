using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Common
{
    // Dummy Impersonation base class. Use this to impersonate current user.
    // This is a helper class and would help in coding when we have to user Current User when privileged User Impersonation is null.
    public class ImpersonateUserBase : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            // Do Nothing. Used to support Using Clause.
        }

        #endregion
    }
}
