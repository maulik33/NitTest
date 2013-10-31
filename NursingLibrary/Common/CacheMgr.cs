using System;
using System.Web;
using System.Web.Caching;

namespace NursingLibrary.Common
{
    public static class CacheMgr
    {
        public delegate T CreateCache<T>();
        public delegate T CreateCacheWithParam<T>(string item);

        private static Cache Cache;
        private static object Synchronizer = new object();

        static CacheMgr()
        {
            Cache = HttpRuntime.Cache;
        }

        public static T Get<T>(string key, CreateCache<T> createCache)
        {
            T cacheObj;

            if (Cache[key] != null && Cache[key].GetType() == typeof(T))
                cacheObj = (T)Cache[key];
            else
            {
                cacheObj = createCache();

                if (!Equals(cacheObj, default(T)))
                    Set(key, cacheObj, TimeSpan.FromMilliseconds(20));
            }

            return cacheObj;
        }

        public static T Get<T>(string key, CreateCacheWithParam<T> createCacheWithParam, string item)
        {
            T cacheObj;

            if (Cache[key] != null && Cache[key].GetType() == typeof(T))
                cacheObj = (T)Cache[key];
            else
            {
                cacheObj = createCacheWithParam(item);

                if (!Equals(cacheObj, default(T)))
                    Set(key, cacheObj, TimeSpan.FromMilliseconds(20));
            }

            return cacheObj;
        }

        public static void Set(string key, object value)
        {
            Set(key, value, TimeSpan.Zero);
        }

        public static void Set(string key, object value, TimeSpan cacheLifetime)
        {
            lock (Synchronizer)
            {
                if (Cache[key] == null)
                    Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, cacheLifetime, CacheItemPriority.Default, null);
                else
                    Cache[key] = value;
            }
        }

        public static void Remove(string key)
        {
            lock (Synchronizer)
            {
                if (Cache[key] != null)
                    Cache.Remove(key);
            }
        }
    }
}