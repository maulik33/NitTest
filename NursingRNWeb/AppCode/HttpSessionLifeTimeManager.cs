using System;
using System.Web;
using Microsoft.Practices.Unity;

/// <summary>
/// Summary description for HttpSessionLifeTimeManager
/// </summary>
public class HttpSessionLifetimeManager : LifetimeManager
{
    private readonly string _key;

    public HttpSessionLifetimeManager()
    {
        _key = Guid.NewGuid().ToString();
    }

    public override object GetValue()
    {
        return HttpContext.Current.Session[_key];
    }
    
    public override void RemoveValue()
    {
        object disposedObj = GetValue();
        if (disposedObj is IDisposable)
        {
            (disposedObj as IDisposable).Dispose();
        }

        HttpContext.Current.Session.Remove(_key);
    }
    
    public override void SetValue(object newValue)
    {
        HttpContext.Current.Session[_key] = newValue;
    }
}
