using System;
using System.Web;
using System.Web.Caching;

namespace NursingLibrary.Utilities
{
    public delegate T CreateCache<T>(object item);

    public static class CacheMgr
    {
        private static readonly Cache Cache = HttpRuntime.Cache;
        private static readonly object Sync = new object();

        public static T Get<T>(string key, CreateCache<T> createCache, TimeSpan expirationTime) where T : class
        {
            T cacheObj;

            if(Cache[key] != null && Cache[key] is T)
            {
                cacheObj = Cache[key] as T;
            }
            else
            {
                cacheObj = createCache(key);

                if(!Equals(cacheObj, default(T)))
                {
                    Set(key, cacheObj, expirationTime);
                }
            }
            return cacheObj;
        }

        public static T Get<T>(string key, CreateCache<T> createCache) where T : class
        {
            T cacheObj;

            if(Cache[key] != null && Cache[key] is T)
            {
                cacheObj = Cache[key] as T;
            }
            else
            {
                cacheObj = createCache(key);

                if(!Equals(cacheObj, default(T)))
                {
                    Set(key, cacheObj);
                }
            }
            return cacheObj;
        }

        public static void Set(string key, object value, TimeSpan cacheLifetime)
        {
            lock(Sync)
            {
                if(value == null && Cache[key] != null)
                {
                    Cache.Remove(key);
                    return;
                }

                if(value != null)
                {
                    Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, cacheLifetime,
                                 CacheItemPriority.Default, null);
                }
            }
        }

        public static void Set(string key, object value)
        {
            lock(Sync)
            {
                if(value == null && Cache[key] != null)
                {
                    Cache.Remove(key);
                }

                if(value != null)
                {
                    Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.Zero,
                                 CacheItemPriority.NotRemovable, null);
                }
            }
        }

        public static void Remove(string key)
        {
            lock(Sync)
            {
                if (Cache[key] != null)
                    Cache.Remove(key);
            }
        }
    } 
}
