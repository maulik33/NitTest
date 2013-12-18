using System;
using System.Web;

namespace NursingLibrary.Utilities
{
    public static class CookieMgr
    {
        public static string CookieName = "KaplanNursingRn";
        public static int CookieTimeout = 24;  // in hours

        public static void SetCookie(string value, bool nonpersistent)
        {
            HttpCookie cookie = new HttpCookie(CookieName, value);

            if (!nonpersistent)
                cookie.Expires = DateTime.Now.AddHours(CookieTimeout);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookie(string value)
        {
            SetCookie(value, false);
        }

        public static string GetCookieValue()
        {
            // check for cookie existence
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            return cookie == null ? null : cookie.Value;
        }

        public static bool CookieExists()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            return cookie != null;
        }

        // convenience cookie access method for user id (inherant to Nursing)
        public static int GetUserId()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if(cookie == null)
                return -1;

            int result;
            if(Int32.TryParse(cookie.Value, out result))
                return result;
            return -1;
        }
    }
}
