using System;
using System.Web;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters.StateManagement
{
    public class CookieManagement : ICookieManagement
    {
        private const string CookieName = "KaplanNursingRn";
        private const int CookieTimeout = 24;  //// in hours

        public void SetCookie(string value, bool nonpersistent)
        {
            var cookie = new HttpCookie(CookieName, value);

            if (!nonpersistent)
            {
                cookie.Expires = DateTime.Now.AddHours(CookieTimeout);
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SetCookie(string value)
        {
            SetCookie(value, false);
        }

        public string GetCookieValue()
        {
            //// check for cookie existence
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            return cookie == null ? null : cookie.Value;
        }

        public bool CookieExists()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            return cookie != null;
        }

        //// convenience cookie access method for user id (inherant to Nursing)
        public int GetUserId()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie == null)
            {
                return -1;
            }

            int result;
            if (Int32.TryParse(cookie.Value, out result))
            {
                return result;
            }

            return -1;
        }
    }
}
