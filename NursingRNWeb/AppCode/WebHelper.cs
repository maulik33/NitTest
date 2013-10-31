using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NursingLibrary.Common;

/// <summary>
/// Summary description for WebHelper
/// </summary>
public class WebHelper
{
    public static string Environment
    {
        get
        {
            return HttpContext.Current.Request.UserAgent;
        }
    }
}