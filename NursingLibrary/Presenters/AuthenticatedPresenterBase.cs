using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public abstract class AuthenticatedPresenterBase<TView> : PresenterBase<TView>
    {
        public AuthenticatedPresenterBase(Module module)
            : base(module)
        {
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
        }

        public override void Authorize()
        {
            bool isAuthorized = false;

            do
            {
                if (CurrentContext == null
                    || CurrentContext.UserId <= 0)
                {
                    break;
                }

                if (CurrentContext.IsAdminLogin)
                {
                    if (CurrentContext.User == null
                        || (false == (CurrentContext.UserType == UserType.AcademicAdmin
                        || CurrentContext.UserType == UserType.LocalAdmin
                        || CurrentContext.UserType == UserType.TechAdmin
                        || CurrentContext.UserType == UserType.InstitutionalAdmin
                        || CurrentContext.UserType == UserType.SuperAdmin)))
                    {
                        break;
                    }
                }
                else
                {
                    if (CurrentContext.UserType != UserType.Student)
                    {
                        break;
                    }
                }

                isAuthorized = true;
            }
            while (false);

            if (false == isAuthorized)
            {
                throw new ApplicationException("User is not Authorized to perform this operation.");
            }
        }

        public bool HasPermission(UserAction action)
        {
            bool hasPermission = false;
            do
            {
                IDictionary<Module, IList<UserAction>> rules;
                if (false == CacheManager.Get<IDictionary<UserType, IDictionary<Module, IList<UserAction>>>>(
                        Constants.CACHE_KEY_AUTH_RULES,
                        PresentationHelper.AuthorizationRulesCallBackDelegate,
                        TimeSpan.FromHours(12)).TryGetValue(CurrentContext.UserType, out rules))
                {
                    break;
                }

                IList<UserAction> allowedActions;
                if (false == rules.TryGetValue(PageModule, out allowedActions))
                {
                    break;
                }

                if (false == allowedActions.Contains(action))
                {
                    break;
                }

                hasPermission = true;
            }
            while (false);

            return hasPermission;
        }
    }
}
