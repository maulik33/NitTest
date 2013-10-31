using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Net;
using NursingLibrary;


public partial class STUDENT_Launchdxr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string cid = Request.QueryString["cid"].ToString(); 
        string eid = Request.QueryString["eid"].ToString();
      //  eid.Value = enrollmentid;
     //   DataTable dtStudentInfo = DataLayer1.GetStudentInfo(Convert.ToInt32(Session["UserID"].ToString())).Tables[0];
     //   string first_name = dtStudentInfo.Rows[0]["FirstName"].ToString();
     //   string last_name = dtStudentInfo.Rows[0]["LastName"].ToString();
        string iid = Request.QueryString["iid"].ToString();
        string ts = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
     //   ts.Value = timestamp;
      //  string st = CreateSHAHash(Global.SecretKey, ts, eid);
     //   st.Value = hash;
  
   WebRequest req = null;
   WebResponse rsp = null;
   try
   {
    string uri = "http://indigo.mercury.dxrnursing.com/cgi-bin/DxR_NP/scripts/index.fcgi";
 
    req = WebRequest.Create(uri);

    string postData = "dxac=kplogin" + "&eid=" + eid + "&cid=" + cid + "&iid=" + iid  + "&ts=" + ts + "&st=" + st;
        //"&first_name=" + first_name + "&last_name=" + last_name;

    //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
    req.Method = "POST";        // Post method
    req.ContentType = "application/x-www-form-urlencoded";     // content type

    byte[] postBuffer = Encoding.UTF8.GetBytes(postData);

       req.ContentLength = postBuffer.Length;
       Stream sPostData = req.GetRequestStream();
       sPostData.Write(postBuffer, 0, postBuffer.Length);
       sPostData.Close();


    rsp = req.GetResponse();
    using (StreamReader sr = new StreamReader(rsp.GetResponseStream()))
    {
        string strResponse = sr.ReadToEnd();

        string strScript = "<script>";
        strScript += " var r = eval("+"(" + strResponse + ")"+");";
         strScript += @" if ( r.status == 1000 ) {
                    document.getElementById('lblError').innerHTML = 'Loading........';
                   self.location.replace(r.message);
                } else 
                {
                 document.getElementById('lblError').innerHTML = r.message;
                }
     　　
     
</script>";
         ClientScript.RegisterStartupScript(typeof(STUDENT_Launchdxr), "kaplain", strScript);
        sr.Close();
    }
      
    
   }
   catch(WebException webEx)
   {
       throw (new Exception("Error", webEx));
   }
   catch(Exception ex)
   {
       throw (new Exception("Error", ex));
   }
   finally
   {
    if(req != null) req.GetRequestStream().Close();
    if(rsp != null) rsp.GetResponseStream().Close();
   }
        
        

    }
    public static string CreateSHAHash(string key, string ts, string enrollid)
    {
        System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
        Byte[] HashAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(key, ts, enrollid));
        Byte[] EncryptedBytes = HashTool.ComputeHash(HashAsByte);
        HashTool.Clear();
    //    return Convert.ToBase64String(EncryptedBytes);
        return BytesToHex(EncryptedBytes);

    }

    public static string BytesToHex(byte[] bytes)
    {
        StringBuilder hexString = new StringBuilder(bytes.Length);
        for (int i = 0; i < bytes.Length; i++)
        {
            hexString.Append(bytes[i].ToString("X2"));
        }
        return hexString.ToString();
    }

}
