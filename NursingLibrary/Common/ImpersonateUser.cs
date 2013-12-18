using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace NursingLibrary.Common
{
    public delegate ImpersonateUserBase AvailUserImpersonation();

    // This is the recommended approach for impersonating user in ASP.net application.
    // http://support.microsoft.com/kb/306158#4
    public class ImpersonateUser : ImpersonateUserBase, IDisposable
    {
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        private WindowsImpersonationContext impersonationContext;

        public ImpersonateUser(String userName, String domain, String password)
        {
            if (false == ImpersonateValidUser(userName, domain, password))
            {
                throw new ApplicationException("User Authentication failed ! Please make sure correct Parameter values are passed.");
            }
        }

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        #region IDisposable Members

        public new void Dispose()
        {
            UndoImpersonation();
        }

        #endregion

        private bool ImpersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }

            if (token != IntPtr.Zero)
            {
                CloseHandle(token);
            }

            if (tokenDuplicate != IntPtr.Zero)
            {
                CloseHandle(tokenDuplicate);
            }

            return false;
        }

        private void UndoImpersonation()
        {
            if (impersonationContext != null)
            {
                impersonationContext.Undo();
            }
        }
    }
}
