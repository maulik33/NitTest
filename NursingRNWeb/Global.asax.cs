using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using Microsoft.Practices.Unity;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;
using NursingRNWeb;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global : HttpApplication, IContainerAccessor
{
    private readonly ILog log = log4net.LogManager.GetLogger(typeof(Global));
    private readonly ILog windowsEventLogger = log4net.LogManager.GetLogger("WindowsEventLogAppender");

    private enum Event
    {
        ApplicationStart,
        ApplicationEnd
    }

    public static bool IsProductionApp { get; set; }

    #region Properties

    /// <summary>
    /// The Unity container for the current application
    /// </summary>
    public static IUnityContainer Container
    {
        get;
        set;
    }

    /// <summary>
    /// Returns the Unity container of the application 
    /// </summary>
    IUnityContainer IContainerAccessor.Container
    {
        get
        {
            return Container;
        }
    }

    #endregion Properties

    #region Methods

    public static bool SuppressKnownWebExceptions(Exception ex)
    {
        var returnValue = false;
        do
        {
            if (ex is HttpException)
            {
                var ex2 = (HttpException)ex;
                if (ex2.GetHttpCode() == 404)
                {
                    returnValue = true;
                    break;
                }
            }

            if (ex is System.Threading.ThreadAbortException)
            {
                returnValue = true;
                break;
            }
        }
        while (false);
        return returnValue;
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        if (Request.RawUrl.IndexOf(".aspx") != -1)
        {
            ThreadContext.Properties["ip"] = string.Format("{0}|{1}|{2}|{3}|{4}|", Request.UserHostAddress, Request.UserAgent, Request.Url.AbsoluteUri, string.Empty, string.Empty);
            log.Debug(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", "Global", "Begin Request", "Entry", DateTime.Now, 0, String.Empty, String.Empty, String.Empty));
            var stopwatch = new Stopwatch();
            HttpContext.Current.Items["Stopwatch"] = stopwatch;
            stopwatch.Start();
        }
    }

    protected void Application_End(object sender, EventArgs e)
    {
        var cache = Container.Resolve<ICacheManagement>();
        if (cache != null)
        {
            cache.Clear();
        }

        LogEvent(Event.ApplicationEnd);
        CleanUp();
    }

    protected void Application_EndRequest(object sender, EventArgs e)
    {
        foreach (var item in
            Container.Registrations.Where(item => item.LifetimeManagerType.Name == typeof(HttpRequestLifetimeManager).Name))
        {
            if (item.LifetimeManager != null)
            {
                item.LifetimeManager.RemoveValue();
            }
        }

        if (Request.RawUrl.IndexOf(".aspx") != -1)
        {
            var stopwatch = (Stopwatch)HttpContext.Current.Items["Stopwatch"];
            stopwatch.Stop();
            log.Debug(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", "Global", "End Request", "Exit", DateTime.Now, stopwatch.Elapsed.TotalSeconds, String.Empty, String.Empty, String.Empty));
        }
    }

    protected void Session_End(object sender, EventArgs e)
    {
        Session.Clear();
    }

    private static void CleanUp()
    {
        if (Container != null)
        {
            Container.Dispose();
        }
    }

    private void Application_Error(object sender, EventArgs e)
    {
        var ex = Server.GetLastError();
        if (false == SuppressKnownWebExceptions(ex))
        {
            if (KTPApp.IsInDebugMode)
            {
                throw ex;
            }
            else
            {
                log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", "Global", "Application_Error", "Error", DateTime.Now, 0, ex.ToString().Replace(Environment.NewLine, ";").Replace("\n", ";"), String.Empty, String.Empty));
                HttpContext.Current.Response.Redirect("~/Error.aspx", true);
            }
        }
    }

    private void Application_Start(object sender, EventArgs e)
    {
        KTPApp.ApplicationRestartDate = System.DateTime.Now;
        XmlConfigurator.ConfigureAndWatch(
            new FileInfo(Server.MapPath("~/log4net.config")));
        try
        {
            BootStrapper.Boot();
            LogEvent(Event.ApplicationStart);
        }
        catch (Exception exception)
        {
            log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", "Global", "Application_Start", "Error", DateTime.Now, 0, exception, String.Empty, String.Empty));
        }
    }
    #endregion Methods

    private void LogEvent(Event logEvent)
    {
        string eventName = logEvent.ToString();
        ThreadContext.Properties["ip"] = string.Format("{0}|{1}|{2}|{3}|{4}|", Environment.MachineName, "Server Event", "N/A", "N/A", "N/A");
        string message = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", "Global", eventName, "Info", DateTime.Now, 0, "Application Event",
            String.Empty, String.Empty);

        log.Error(message);

        message = string.Format("Application has triggered {0} event", eventName);
        windowsEventLogger.Info(message);
    }
}
