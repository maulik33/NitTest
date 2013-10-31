using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class LtiProviderPresenter : AuthenticatedPresenterBase<ILtiProviderView>
    {
        private readonly IAdminService _adminService;

        public LtiProviderPresenter(IAdminService service)
            : base(Module.Others)
        {
            _adminService = service;
        }

        public override void RegisterQueryParameters()
        {
            RegisterQueryParameter(QUERY_PARAM_ACTION_TYPE);
            RegisterQueryParameter(QUERY_PARAM_ID, QUERY_PARAM_ACTION_TYPE, ((int)UserAction.Edit).ToString(CultureInfo.InvariantCulture));
        }

        public void GetLtiProviders()
        {
            if (ActionType == UserAction.Edit || ActionType == UserAction.View)
            {
                string ltiProviderId = GetParameterValue(QUERY_PARAM_ID);
                int ltiProviderIdInt;
                if (int.TryParse(ltiProviderId, out ltiProviderIdInt))
                {
                    List<LtiProvider> ltiProviders = _adminService.GetLtiProviders(ltiProviderIdInt);
                    LtiProvider ltiProvider = ltiProviders[0];
                    switch (ActionType)
                    {
                        case UserAction.Edit:
                            View.BindDataOnEdit(ltiProvider);
                            break;
                        case UserAction.View:
                            View.BindDataOnView(ltiProvider);
                            break;
                    }
                }

            }
            else
            {
                List<LtiProvider> ltiProviders = _adminService.GetLtiProviders();
                View.BindData(ltiProviders);
 
            }
        }

        public void SaveLtiProvider(LtiProvider lti)
        {
            if (ActionType == UserAction.Edit)
            {
                int ltiId = GetLtiProviderIdFromQuery();
                lti.Id = ltiId;
            }
            _adminService.SaveLtiProvider(lti);
            NavigateToList();

        }

        public void ChangeLtiProviderStatus(int ltiProviderId)
        {
            _adminService.ChangeLtiProviderStatus(ltiProviderId);
            GetLtiProviders();
        }

        private int GetLtiProviderIdFromQuery()
        {
            string ltiProviderId = GetParameterValue(QUERY_PARAM_ID);
            int ltiProviderIdInt;
            if (int.TryParse(ltiProviderId, out ltiProviderIdInt))
            {
                return ltiProviderIdInt;
            }
            throw new Exception("Unexpected lti id");
        }

        public bool LtiNameExists(string providerName)
        {
            int ltiId = 0;  //value of 0 works if we're adding
            if (ActionType == UserAction.Edit)
            {
                ltiId = GetLtiProviderIdFromQuery();
            }
            List<LtiProvider> ltiProviders = _adminService.GetLtiProviders(); //get all providers
            int count = (from l in ltiProviders
                         where l.Name == providerName
                         && l.Id != ltiId
                         select l).Count();
            return count > 0;
        }
        
       public void NavigateToEdit(int ltiProviderId)
	{
		var nav = new Navigation.PageNavigator();
		nav.NavigateTo(AdminPageDirectory.LtiProviders, string.Empty, string.Format("{0}={1}&{2}={3}",QUERY_PARAM_ACTION_TYPE, ((int)UserAction.Edit).ToString(CultureInfo.InvariantCulture), QUERY_PARAM_ID, ltiProviderId));
	}

       public void NavigateToView(int ltiProviderId)
       {
	   var nav = new Navigation.PageNavigator();
	   nav.NavigateTo(AdminPageDirectory.LtiProviders, string.Empty, string.Format("{0}={1}&{2}={3}", QUERY_PARAM_ACTION_TYPE, ((int)UserAction.View).ToString(CultureInfo.InvariantCulture), QUERY_PARAM_ID, ltiProviderId));
       }

       public void NavigateToList()
       {
	   var nav = new Navigation.PageNavigator();
	   nav.NavigateTo(AdminPageDirectory.LtiProviders, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ACTION_TYPE, ((int)UserAction.Add).ToString(CultureInfo.InvariantCulture)));
       }

	  public void NavigateToLogin()
	  {
	      Navigator.NavigateTo(AdminPageDirectory.AdminLogin, null, null);
	  }

    }
}
