using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NursingLibrary.Utilities
{
    public class CustomServerHeaderModule : IHttpModule
    {
        private readonly List<string> headersToClean;

        public CustomServerHeaderModule()
        {
            headersToClean = new List<string>
                                 {
                                     "Server",
                                     "X-AspNet-Version",
                                     "X-Powered-By"
                                 };
        }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose()
        {
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            headersToClean.ForEach(header => HttpContext.Current.Response.Headers.Remove(header));
        }
    }
}
