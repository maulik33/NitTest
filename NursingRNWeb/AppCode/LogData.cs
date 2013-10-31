using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

[Serializable]
public class LogData
{
    public string UserHostAddress { get; set; }

    public string UserAgent { get; set; }

    public string URL { get; set; }

    public string SessionId { get; set; }

    public string UserName { get; set; }

    public string ClassName { get; set; }

    public string MethodName { get; set; }

    public string ProcessedType { get; set; }

    public DateTime DateLogged { get; set; }

    public string ExecutionTimeInMilliS { get; set; }

    public string Exception { get; set; }

    public string InputParameters { get; set; }

    public string OutPutParameters { get; set; }
}