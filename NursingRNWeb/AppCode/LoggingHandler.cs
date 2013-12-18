using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

/// <summary>
/// Summary description for LoggingHandler
/// </summary>
[ConfigurationElementType(typeof(CustomCallHandlerData))] // this decorator is needed to make the call handler show up in the Configuration Manager tool
public class LoggingHandler : ICallHandler
{
    private readonly Profiler _profiler;
    private readonly bool detailedLog;

    public LoggingHandler()
    {
        _profiler = new Profiler();

        // detailedLog = HttpContext.Current.Session["DetailedLog"] != null ||  HttpContext.Current.Session["DetailedLog"].ToString() != string.Empty;
        if (HttpContext.Current.Session != null)
        {
            var sessionLog = HttpContext.Current.Session["DetailedLog"] as string;
            detailedLog = !String.IsNullOrEmpty(sessionLog) && (sessionLog == "1");
        }
    }

    /// <summary>
    /// Required by the interface, but we don't use the parameter
    /// </summary>
    /// <param name="ignore"></param>
    public LoggingHandler(NameValueCollection ignore)
    {
    }

    public int Order
    {
        get;
        set;
    }

    public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
    {
        var log = LogManager.GetLogger(input.Target.GetType());

        var arguments = (from object parameter in input.Arguments select (parameter != null) ? parameter.ToString() : "null").ToList();
        if (detailedLog)
        {
            log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", input.MethodBase.DeclaringType.Name, input.MethodBase.Name, "Entry", DateTime.Now, 0, string.Empty, string.Join(";", arguments.ToArray()), String.Empty));
        }
        else
        {
            log.Debug(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", input.MethodBase.DeclaringType.Name, input.MethodBase.Name, "Entry", DateTime.Now, 0, string.Empty, string.Join(";", arguments.ToArray()), String.Empty));
        }

        // var newMethodName = _profiler.Start(input.MethodBase.DeclaringType.Name + input.MethodBase.Name + string.Join(";", arguments.ToArray()));
        var newMethodName = _profiler.Start(input.MethodBase.DeclaringType.Name + input.MethodBase.Name);
        var result = getNext()(input, getNext);
        var totalTime = _profiler.Stop(newMethodName);

        // Post method call processing  
        if (result.Exception != null
            && false == (result.Exception is ApplicationException
            || result.Exception is ThreadAbortException))
        {
            // if (result.Exception != null)
            var exceptionMsg = string.Empty;

            if (input.Target.GetType().Name.IndexOf("Repository") != -1 && (result.Exception is SqlException))
            {
                exceptionMsg = result.Exception.ToString();
            }

            if (input.Target.GetType().Name.IndexOf("Repository") == -1 && !(result.Exception is SqlException))
            {
                exceptionMsg = result.Exception.ToString();
            }
            else
            {
                exceptionMsg = string.Format("{0} - {1}{2}", result.Exception.GetType().ToString(), result.Exception.Message, result.Exception.StackTrace);
            }

            log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", input.MethodBase.DeclaringType.Name, input.MethodBase.Name, "Error", DateTime.Now, totalTime, exceptionMsg.Replace(Environment.NewLine, ";").Replace("\n", ";"), string.Empty, String.Empty));

            ApplicationException xp = new ApplicationException(exceptionMsg);
            throw xp;
        }
        else
        {
            var output = string.Empty;
            if (input.Target.GetType().Name.IndexOf("Context") == -1)
            {
                output = (result.ReturnValue != null) ? DisplayObjectProperties(result.ReturnValue) : string.Empty;
            }

            if (detailedLog)
            {
                log.Error(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", input.MethodBase.DeclaringType.Name, input.MethodBase.Name, "Exit", DateTime.Now, totalTime, string.Empty, string.Empty, output));
            }
            else
            {
                log.Debug(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", input.MethodBase.DeclaringType.Name, input.MethodBase.Name, "Exit", DateTime.Now, totalTime, string.Empty, string.Empty, output));
            }
        }

        return result;
    }

    private static string DisplayObjectProperties(Object o)
    {
        var objectString = new List<string>();
        var type = o.GetType();

        foreach (var p in type.GetProperties().Where(p => p.CanRead))
        {
            try
            {
                var obj = p.GetValue(o, null);
                objectString.Add(obj != null
                                     ? string.Format("{0}:{1}", p.Name, obj)
                                     : string.Format("{0}:{1}", p.Name, "null"));
            }
            catch (Exception ex)
            {
                objectString.Add(string.Format("{0}:{1}", p.Name, ex.Message));
            }
        }

        return string.Join(";", objectString.ToArray());
    }
}
