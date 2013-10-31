using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Navigation;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    /// <summary>
    /// http://www.pnpguidance.net/Post/UnityIoCDependencyInjectionASPNETModelViewPresenter.aspx
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class PresenterBase<TView>
    {
        #region Fields
        public const string QUERY_PARAM_ID = "Id";
        public const string QUERY_PARAM_ACTION_TYPE = "ActionType";

        private readonly Dictionary<string, QueryParamInfo> _queryParams;
        private readonly List<string> _authorizedActions;
        private readonly Module _pageModule;
        private ExecutionContext _executionContext;
        #endregion Fields

        #region Constructors

        public PresenterBase(Module module)
        {
            _queryParams = new Dictionary<string, QueryParamInfo>();
            _authorizedActions = new List<string>();
            _pageModule = module;
        }

        #endregion Constructors

        #region Properties

        public Module PageModule
        {
            get
            {
                return _pageModule;
            }
        }

        public ICacheManagement CacheManager { get; set; }

        public ISessionManagement SessionManager { get; set; }

        public IPageNavigator Navigator { get; set; }

        public int Id { get; set; }

        public UserAction ActionType { get; set; }

        public string SortingRule { get; set; }

        public ExecutionContext CurrentContext
        {
            get
            {
                return _executionContext;
            }
        }

        public TView View
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public abstract void RegisterAuthorizationRules();

        public abstract void RegisterQueryParameters();

        public void RegisterOptionalQueryParameter(string key)
        {
            RegisterQueryParameter(key, true);
        }

        public IEnumerable<string> GetQueryParameters(bool onlyCriteria)
        {
            return onlyCriteria
                ? _queryParams.Where(p => false == string.IsNullOrEmpty(p.Value.Criteria.Key))
                .Select(p => p.Key).AsEnumerable()
                : _queryParams.Keys.AsEnumerable();
        }

        public string GetParameterValue(string key)
        {
            return _queryParams[key].Value;
        }

        public void SetParameterValue(string key, string value)
        {
            _queryParams[key].Value = value;
            if (key == QUERY_PARAM_ID)
            {
                Id = value.ToInt();
            }
            else if (key == QUERY_PARAM_ACTION_TYPE)
            {
                ActionType = EnumHelper.GetUserAction(value);
            }
        }

        public KeyValuePair<string, string> GetParameterCriteria(string key)
        {
            return _queryParams[key].Criteria;
        }

        public abstract void Authorize();

        public virtual void OnViewError()
        {
            Navigator.NavigateTo(AdminPageDirectory.Error, string.Empty, string.Empty);
        }

        public void Initialize()
        {
            _executionContext = SessionManager.Get<ExecutionContext>(SessionKeys.EXECUTION_CONTEXT);
        }

        public void ShowError()
        {
            Navigator.NavigateTo(AdminPageDirectory.AdminLogin, null, null);
        }

        protected void RegisterAuthorizationRule(UserAction action)
        {
            RegisterAuthorizationRule(PageModule, action);
        }

        protected void RegisterAuthorizationRule(Module module, UserAction action)
        {
            _authorizedActions.Add(string.Format("{0}.{1}", module, action));
        }

        protected void RegisterQueryParameter(string key)
        {
            RegisterQueryParameter(key, false);
        }

        protected void RegisterQueryParameter(string key, string criteriaKey, string criteriaValue)
        {
            RegisterQueryParameter(key, false)
                .Criteria = new KeyValuePair<string, string>(criteriaKey, criteriaValue);
        }

        private QueryParamInfo RegisterQueryParameter(string key, bool isOptional)
        {
            QueryParamInfo parameter = new QueryParamInfo() { IsOptional = isOptional };
            _queryParams.Add(key, parameter);
            return parameter;
        }
        #endregion Methods

        private class QueryParamInfo
        {
            public QueryParamInfo()
            {
                Criteria = new KeyValuePair<string, string>();
            }

            public string Value { get; set; }

            public KeyValuePair<string, string> Criteria { get; set; }
            
            public bool IsOptional { get; set; }
        }
    }
}