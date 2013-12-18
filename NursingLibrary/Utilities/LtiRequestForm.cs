using System.Text;
using System.Web;
using LtiLibrary.Consumer;

namespace NursingLibrary.Utilities
{
    /// <summary>
    /// LTI post request form builder
    /// </summary>
    public class LtiRequestForm
    {
        public static string BuildPostRequestForm(LtiOutboundRequestViewModel ltiRequst, string formTarget = "target = '_blank'")
        {
            HttpContext.Current.Response.Clear();
            var sb = new StringBuilder();
            sb.AppendFormat(string.Format("<form id='form' action='{0}' method='post' {1}>", ltiRequst.Action, formTarget));
            foreach (var name in ltiRequst.Fields.AllKeys)
            {
                sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", name, ltiRequst.Fields[name]);
            }
            sb.AppendFormat("<input type='hidden' name='oauth_signature' value='{0}'>", ltiRequst.Signature);
            sb.Append("</form>");
            sb.Append("<script>");
            sb.Append("$(function () {$('#form').submit();});");
            // sb.Append("$('#form').submit(function() {return false;});");
            //sb.Append("$.post('"+ltiRequst.Action +"', $('#form').serialize(), function(result){alert('callback called')});");
            // sb.Append("window.open('resultPageURL&result='+encodeURIComponent(result), '_blank');");
            // sb.Append("});");
            sb.Append("</script>");
            return sb.ToString();
        }
    }
}