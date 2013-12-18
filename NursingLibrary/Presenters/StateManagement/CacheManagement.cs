using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters.StateManagement
{
    /// <summary>
    /// How to cache data in a MVC application
    /// http://stackoverflow.com/questions/343899/how-to-cache-data-in-a-mvc-application
    /// </summary>
    public class CacheManagement : ICacheManagement
    {
        public T Get<T>(string key, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(key) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpRuntime.Cache.Insert(key, item);
            }
            
            return item;
        }

        public T Get<T>(string key, Func<T> getItemCallback, TimeSpan expirationTime) where T : class
        {
            var item = HttpRuntime.Cache.Get(key) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpRuntime.Cache.Insert(key, item, null, Cache.NoAbsoluteExpiration, expirationTime);
            }

            return item;
        }

        public T GetNotRemovableItem<T>(string key, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(key) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpRuntime.Cache.Insert(key, item, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
            }

            return item;
        }

        /// <summary>
        /// Removes all items from the Cache
        /// Note - should not remove from cache in while loop since enumerator is only valid while collection remaind intact:
        /// http://aspadvice.com/blogs/ssmith/archive/2005/11/14/Extending-the-ASPNET-Cache-Object-Cache-Clear.aspx
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemcollectionsidictionaryenumeratorclasstopic.asp
        /// </summary>
        public void Clear()
        {
            var keyList = new List<string>();
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                keyList.Add(cacheEnum.Key.ToString());
            }

            foreach (string key in keyList)
            {
                object disposedObj = HttpRuntime.Cache.Get(key);
                if (disposedObj is IDisposable)
                {
                    (disposedObj as IDisposable).Dispose();
                }

                HttpContext.Current.Cache.Remove(key);
            }
        }
    } 
}
