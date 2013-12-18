using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using log4net;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb;

/// <summary>
/// http://www.pnpguidance.net/Post/UnityIoCDependencyInjectionASPNETModelViewPresenter.aspx
/// </summary>
public abstract class PageBase<TView, TPresenter> : Page
    where TView : class
    where TPresenter : PresenterBase<TView>
{
    private readonly ILog log = log4net.LogManager.GetLogger(typeof(TView));

    public TPresenter Presenter { get; set; }

    public static object Resolve<T>()
    {
        return Global.Container.Resolve(typeof(T), string.Empty);
    }

    public abstract void PreInitialize();

    protected override void OnInit(EventArgs e)
    {
        if (!this.DesignMode)
        {
            InjectDependencies();
            PreInitialize();
            ThreadContext.Properties["ip"] = string.Format("{0}|{1}|{2}|{3}|{4}|", Request.UserHostAddress,
                Request.UserAgent, Request.Url.AbsoluteUri, Session.SessionID,
                Session["UserName"] ?? Request.Form["TxtUserName"]);
            Session["DetailedLog"] = Request.QueryString["DetailedLog"];
            Presenter.RegisterAuthorizationRules();
            Presenter.RegisterQueryParameters();
            Presenter.Initialize();
            MasterPageBase customMasterPageBase = this.Master as MasterPageBase;
            if (customMasterPageBase != null)
            {
                customMasterPageBase.CurrentContext = Presenter.CurrentContext;
                customMasterPageBase.Navigator = Presenter.Navigator;
                RegisterUserControls(customMasterPageBase.RegisteredControls.ToArray());
            }
            
            ValidateQueryParameters();
        }

        base.OnInit(e);
    }

    protected override void OnPreLoad(EventArgs e)
    {
        Presenter.Authorize();
        base.OnPreLoad(e);
    }

    protected void RegisterUserControls(params UserControlBase[] controls)
    {
        foreach (var control in controls)
        {
            control.CurrentContext = Presenter.CurrentContext;
            control.Navigator = Presenter.Navigator;
        }
    }

    protected virtual void InjectDependencies()
    {
        Presenter = Resolve<TPresenter>() as TPresenter;
        if (Presenter == null)
        {
            throw new InvalidOperationException("Presenter could not be resolved");
        }

        Presenter.View = this as TView;
    }

    protected override void OnError(EventArgs e)
    {
        base.OnError(e);
        Exception objErr = HttpContext.Current.Server.GetLastError().GetBaseException();
        if (objErr.Source.IndexOf("Unity") == -1)
        {
            if (Presenter.CurrentContext != null
                && Presenter.CurrentContext.TraceToken != null
                && objErr != null)
            {
                TraceHelper.WriteTraceError(Presenter.CurrentContext.TraceToken, objErr.Message);
            }

            log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", GetType().Name, "Page_Error", "Error", DateTime.Now, 0, objErr.ToString().Replace(Environment.NewLine, ";").Replace("\n", ";"), String.Empty, String.Empty));
        }

        if (KTPApp.IsInDebugMode)
        {
            return;
        }

        HttpContext.Current.Server.ClearError();
        Presenter.OnViewError();
    }

    private void ValidateQueryParameters()
    {
        ////TODO: Gokul - Convert to left join. Check how multiple conditions can be specified in left join.

        //// Add Query Params that needs to be checked always.
        List<string> paramsToCheck = Presenter.GetQueryParameters(false)
            .Except(Presenter.GetQueryParameters(true))
            .ToList();

        //// Add Query Params that needs to be checked based on a condition.
        foreach (string key in Presenter.GetQueryParameters(true))
        {
            if (string.Compare(Request.QueryString[Presenter.GetParameterCriteria(key).Key], Presenter.GetParameterCriteria(key).Value, true) == 0)
            {
                paramsToCheck.Add(key);
            }
        }

        var missingQueryParams = paramsToCheck.Except(Request.QueryString.AllKeys, StringComparer.OrdinalIgnoreCase).ToArray();

        if (missingQueryParams.Length > 0)
        {
            throw new ApplicationException(string.Format("Invalid URL. Parameter(s) missing : {0}", string.Join(",", missingQueryParams.ToArray())));
        }

        // Add parameters that are not added through Register method.
        var optionalParams = Request.QueryString.AllKeys.Except(Presenter.GetQueryParameters(false));
        foreach (string key in optionalParams)
        {
            Presenter.RegisterOptionalQueryParameter(key);
        }

        foreach (string key in Presenter.GetQueryParameters(false))
        {
            Presenter.SetParameterValue(key, Request.QueryString[key] ?? string.Empty);
        }
    }
}