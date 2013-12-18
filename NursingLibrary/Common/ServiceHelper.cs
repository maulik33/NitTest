using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Common
{
    public class ServiceHelper
    {
        private readonly IAdminService _adminService;

        public ServiceHelper(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAuthorizationRules()
        {
            return _adminService.GetAuthorizationRules();
        }

        public IDictionary<AppSettings, string> GetAppSettings()
        {
            return _adminService.GetAppSettings();
        }

        public void SaveAppSetting(AppSettings setting, string value)
        {
            _adminService.SaveAppSetting(setting, value);
        }
    }
}
