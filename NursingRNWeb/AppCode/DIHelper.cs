using System.Collections.Generic;
using Microsoft.Practices.Unity;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;

namespace NursingRNWeb
{
    public static class DIHelper
    {
        public static IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAuthorizationRules()
        {
            var serviceHelper = GetServiceHelperInstance();
            return serviceHelper.GetAuthorizationRules();
        }

        public static ServiceHelper GetServiceHelperInstance()
        {
            return Global.Container.Resolve<ServiceHelper>();
        }
    }
}