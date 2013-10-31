using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;
using NursingLibrary.Common;

/// <summary>
/// Summary description for ElmahWrapper
/// </summary>
public class ElmahWrapper : ErrorLogModule
{
    protected override void OnInit(HttpApplication application)
    {
        if (false == KTPApp.IsInDebugMode)
        {
            base.OnInit(application);
        }
    }
}