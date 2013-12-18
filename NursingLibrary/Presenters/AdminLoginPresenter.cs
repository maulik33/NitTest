using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class AdminLoginPresenter : UnAuthenticatedPresenterBase<IAdminLogin>
    {
        private readonly IAdminService _adminService;

        public AdminLoginPresenter(IAdminService service)
        {
            _adminService = service;
        }

        public void AuthenticateUser(string userName, string userPassword, string environment)
        {
            var _result = _adminService.AuthenticateUser(userName, userPassword);
            if (_result.Status == AuthenticationRequest.Success)
            {
                TraceToken traceToken = TraceHelper.BeginTrace(_result.User.UserId, userName, environment);
                ExecutionContext context = new ExecutionContext()
                {
                    IsAdminLogin = true,
                    UserId = _result.User.UserId,
                    UserType = EnumHelper.GetUserType(_result.User.SecurityLevel),
                    User = _result.User,
                    TraceToken = traceToken
                };
                SessionManager.Set<ExecutionContext>(SessionKeys.EXECUTION_CONTEXT, context);
                Navigate(context.UserType);
            }
            else
            {
                View.ShowMessage(_result);
            }
        }

        public LoginContent GetLoginContent()
        {
            return _adminService.GetLoginContent((int)LoginContents.Admin);
        }

        private void Navigate(UserType userRole)
        {
            switch (userRole)
            {
                case UserType.LocalAdmin:
                case UserType.AcademicAdmin:
                case UserType.SuperAdmin:
                case UserType.InstitutionalAdmin:
                case UserType.TechAdmin:
                    Navigator.NavigateTo(AdminPageDirectory.AdminHome);
                    break;
                default:
                    Navigator.NavigateTo(AdminPageDirectory.AdminLogin);
                    break;
            }
        }
    }
}
