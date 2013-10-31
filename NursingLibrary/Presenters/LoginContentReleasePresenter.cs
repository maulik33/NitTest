using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class LoginContentReleasePresenter : AuthenticatedPresenterBase<ILoginContentReleaseView>
    {
        private readonly ICMSService _cmsService;
        private ViewMode _mode;

        public LoginContentReleasePresenter(ICMSService service)
            : base(Module.CMS)
        {
            _cmsService = service;
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void SaveLoginContents(LoginContent loginContent)
        {
            loginContent.AdminUserId = CurrentContext.UserId;
            loginContent.ReleaseStatus = "E";
            _cmsService.SaveLoginContents(loginContent);
        }

        public LoginContent GetLoginContent(int contentId)
        {
            return _cmsService.GetLoginContent(contentId);
        }

        public void ReleaseLoginContent(LoginContent loginContent)
        {
            loginContent.AdminUserId = CurrentContext.UserId;
            loginContent.ReleaseStatus = "R";
            _cmsService.ReleaseLoginContent(loginContent);
        }

        public void RevertLoginContent(LoginContent loginContent)
        {
            loginContent.AdminUserId = CurrentContext.UserId;
            _cmsService.RevertLoginContent(loginContent);
        }
    }
}
