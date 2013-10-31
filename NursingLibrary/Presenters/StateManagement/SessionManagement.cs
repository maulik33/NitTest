using System.Web;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters.StateManagement
{
    public class SessionManagement : ISessionManagement
    {
        public T Get<T>(string key) where T : class
        {
            return HttpContext.Current.Session[key] as T;
        }

        public void Set<T>(string key, T item)
        {
            HttpContext.Current.Session[key] = item;
        }
    }
}
