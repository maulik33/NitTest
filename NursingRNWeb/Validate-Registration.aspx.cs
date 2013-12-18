using System;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ValidateRegistration : PageBase<IValidateNursingView, ValidateRegistrationPresenter>, IValidateNursingView
{
    private TraceToken _traceToken;

    public ValidateNursingParams GetQueryParameters()
    {
        var valParams = new ValidateNursingParams();
        valParams.UserId = Request.Form["USERID"] ?? string.Empty;
        valParams.ProductCode = Request.Form["PRODUCTCODE"] ?? string.Empty;
        valParams.CourseAccessId = Request.Form["COURSE_ACCESS_ID"] ?? "0";
        valParams.Command = Request.Form["COMMAND"] ?? string.Empty;
        valParams.FirstName = Request.Form["FIRSTNAME"] ?? string.Empty;
        valParams.LastName = Request.Form["LASTNAME"] ?? string.Empty;
        valParams.Products = Request.Form["PRODUCTS"] ?? string.Empty;
        valParams.Email = Request.Form["EMAIL"] ?? string.Empty;
        valParams.FacilityId = Request.Form["FACILITYID"] ?? "0";

        TraceHelper.Create(_traceToken, "Validate Registration Get Query Parameters")
            .Add("UserId", valParams.UserId)
            .Add("ProductCode", valParams.ProductCode)
            .Add("CourseAccessId", valParams.CourseAccessId)
            .Add("Command", valParams.Command)
            .Add("FirstName", valParams.FirstName)
            .Add("LastName", valParams.LastName)
            .Add("Products", valParams.Products)
            .Add("Email", valParams.Email)
            .Add("FacilityId", valParams.FacilityId)
            .Write();

        return valParams;
    }

    public void SendHttpresponse()
    {
        TraceHelper.Create(_traceToken, "Send HTTP Response")
            .Add("Response", "HTTP/1.1 206 400,700,1,1")
            .Write();
        Response.Write("HTTP/1.1 206 400,700,1,1<br/>");
    }

    public void SendRStatus(string rStatus)
    {
        TraceHelper.Create(_traceToken, "Send R Status")
            .Add("Response", rStatus)
            .Write();

        Response.Write(rStatus);
    }

    public override void PreInitialize()
    {
        _traceToken = TraceHelper.BeginTrace(0, "System", "Unknown");
        TraceHelper traceMessage = TraceHelper.Create(_traceToken, "Integration");

        try
        {
            traceMessage.Add("URL", Request.Url.ToString());
        }
        catch (Exception ex)
        {
            traceMessage.Add("Error.1", ex.Message);
        }

        try
        {
            traceMessage.Add("Raw URL", Request.RawUrl);
        }
        catch (Exception ex)
        {
            traceMessage.Add("Error.2", ex.Message);
        }

        try
        {
            if (Request.UrlReferrer != null)
            {
                traceMessage.Add("URL Referrer", Request.UrlReferrer.ToString());
            }
        }
        catch (Exception ex)
        {
            traceMessage.Add("Error.3", ex.Message);
        }

        foreach (string key in Request.Form.AllKeys)
        {
            try
            {
                traceMessage.Add(key, Request.Form[key] ?? string.Empty);
            }
            catch (Exception ex)
            {
                traceMessage.Add("Error", ex.Message);
            }
        }

        traceMessage.Write();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.ShowCommandValues();
    }
}
