using System;
using System.Web;
using Microsoft.Practices.Unity;

    /// <summary>
    /// Summary description for HttpRequestLifetimeManager
    /// </summary>
    public class HttpRequestLifetimeManager : LifetimeManager
    {
        private readonly string _key;

        public HttpRequestLifetimeManager()
        {
            _key = "Reg" + Guid.NewGuid();
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_key];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_key] = newValue;
        }

        public override void RemoveValue()
        {
            object disposedObj = GetValue();
            if (disposedObj is IDisposable)
            {
                (disposedObj as IDisposable).Dispose();
            }

            HttpContext.Current.Items.Remove(_key);
        }
    }
