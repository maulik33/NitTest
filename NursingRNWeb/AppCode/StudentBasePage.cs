using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using log4net;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

/// <summary>
/// http://www.pnpguidance.net/Post/UnityIoCDependencyInjectionASPNETModelViewPresenter.aspx
/// </summary>
public abstract class StudentBasePage<TView, TPresenter> : Page
    where TPresenter : StudentPresenter<TView>
    where TView : class
{
    private readonly ILog log = LogManager.GetLogger(typeof(TView));

    public Student Student
    {
        get
        {
            return Presenter.Student;
        }
    }

    public TPresenter Presenter { get; set; }

    public TraceToken TraceToken
    {
        get
        {
            return Presenter.TraceToken;
        }
    }

    public static object Resolve<T>()
    {
        var context = HttpContext.Current;
        if (context == null)
        {
            throw new InvalidOperationException("Context is null");
        }

        var accessor = context.ApplicationInstance as IContainerAccessor;
        if (accessor == null)
        {
            throw new InvalidOperationException("ContainerAccessor is null");
        }

        var container = accessor.Container;
        if (container == null)
        {
            throw new InvalidOperationException("No Unity container found");
        }

        return container.Resolve(typeof(T), string.Empty);
    }

    /*
    public void ShowPage(PageDirectory page,string fragment,string query)
    {
        Presenter.ShowPage(page, fragment, query);
    }
    */

    public TableRow CreateSvLinks(int productid, int testSubGroup, string linkName)
    {
        IEnumerable<AvpContent> svLinks = Presenter.GetAvpContent(productid, testSubGroup);
        var r = new TableRow();
        if (svLinks.Count() != 0)
        {
            var c = new TableCell();
            r.Cells.Add(c);
            c.Attributes.Add("style", "width:50%");
            c.Text = svLinks.FirstOrDefault().TestName;

            c = new TableCell { HorizontalAlign = HorizontalAlign.Right };
            r.Cells.Add(c);
            var l = new HyperLink { Text = linkName, CssClass = "s2" };

            string assetCode = string.Empty;
            string productCode;
            if (testSubGroup == 6)
            {
                assetCode = "main/nclex/comparative/nclexDiagnostic";
                productCode = "NCLEX2008";
            }
            else if (testSubGroup == 7)
            {
                assetCode = "main/nclex/comparative/nclexDiagnostic";
                productCode = "NCLEX2008";
            }
            else if (testSubGroup == 8)
            {
                assetCode = "main/nclex/comparative/nclexDiagnostic";
                productCode = "NCLEX2008";
            }
            else if (testSubGroup == 9)
            {
                assetCode = "main/nclex/comparative/nclexReadiness";
                productCode = "NCLEX2008";
            }
            else
            {
                productCode = "NursingIntegrated";
            }

            l.NavigateUrl = "#";

            l.Attributes.Add("onclick",
                             string.Format(
                                 "window.open('launchTest.aspx?ProfileID={0}&assetCode={1}&productCode={2}&enrollmentID={3}','Nursing')",
                                 Student.KaplanUserId, assetCode, productCode, Student.EnrollmentId));
            c.Controls.Add(l);
        }

        return r;
    }

    //// ReSharper disable InconsistentNaming
    protected void Page_Init(object sender, EventArgs e)
    //// ReSharper restore InconsistentNaming
    {
        InjectDependencies();
        if (Header != null)
        {
            var cssLink = new HtmlLink { Href = "~/favicon.ico" };
            cssLink.Attributes.Add("rel", "shortcut icon");
            Header.Controls.Add(cssLink);
        }

        ThreadContext.Properties["ip"] = string.Format("{0}|{1}|{2}|{3}|{4}|", Request.UserHostAddress, Request.UserAgent, Request.Url.AbsoluteUri, Session.SessionID, Session["UserName"] ?? Request.Form["TxtUserName"]);
        Session["DetailedLog"] = Request.QueryString["DetailedLog"];
    }

    protected virtual void InjectDependencies()
    {
        Presenter = Resolve<TPresenter>() as TPresenter;
        //// Presenter = container.Resolve(typeof(TPresenter), string.Empty) as TPresenter;
        if (Presenter == null)
        {
            throw new InvalidOperationException("Presenter could not be resolved");
        }

        Presenter.View = this as TView;
    }

    //// ReSharper disable InconsistentNaming
    protected void Page_Error(object sender, EventArgs e)
    //// ReSharper restore InconsistentNaming
    {
        Exception objErr = HttpContext.Current.Server.GetLastError().GetBaseException();
        if (objErr.Source.IndexOf("Unity") == -1)
        {
            if (TraceToken != null && objErr != null)
            {
                TraceHelper.WriteTraceError(TraceToken, objErr.Message);
            }

            log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", GetType().Name, "Page_Error", "Error", DateTime.Now, 0, objErr.ToString().Replace(Environment.NewLine, ";").Replace("\n", ";"), String.Empty, String.Empty));
        }

        if (KTPApp.IsInDebugMode)
        {
            return;
        }

        HttpContext.Current.Server.ClearError();
        OnError(objErr);
    }

    //// ReSharper disable InconsistentNaming
    protected void Page_UnLoad(object sender, EventArgs e)
    //// ReSharper restore InconsistentNaming
    {
        if (Presenter != null)
        {
            Presenter.OnViewUnload();
        }
    }

    protected virtual void OnError(Exception exception)
    {
        Presenter.OnViewError(exception);
    }
}
