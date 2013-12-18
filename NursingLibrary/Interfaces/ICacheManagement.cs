using System;
using System.Web.Caching;

namespace NursingLibrary.Interfaces
{
    public interface ICacheManagement
    {
        T Get<T>(string key, Func<T> getItemCallback) where T : class;

        T Get<T>(string key, Func<T> getItemCallback, TimeSpan expirationTime) where T : class;

        T GetNotRemovableItem<T>(string key, Func<T> getItemCallback) where T : class;

        void Clear();
    }
}
