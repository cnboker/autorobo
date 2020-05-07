using System.Threading;

namespace System.Web.Caching
{
    /// <summary>
    ///  var all = Cache.Data("menuCache", 300, () =>
    ///{
    ///     return db.Query("SELECT * FROM MENU ");
    /// });
    /// </summary>
    public static class CacheExtensions
    {
        static public bool EnableCache { get; set; }

        /// <summary>
        /// 缓存时间,缺省5分钟
        /// </summary>
        static public int CacheSeconds = 300;

        static object sync = new object();
        private static HttpRuntime _httpRuntime;

        private static void EnsureHttpRuntime()
        {
            if (null == _httpRuntime)
            {
                try
                {
                    Monitor.Enter(sync);
                    var enableCache = System.Configuration.ConfigurationManager.AppSettings["eanbleCache"];
                    EnableCache = string.IsNullOrEmpty(enableCache) ? true : Convert.ToBoolean(enableCache);

                    if (null == _httpRuntime)
                    {
                        // Create an Http Content to give us access to the cache.
                        _httpRuntime = new HttpRuntime();
                    }
                }
                finally
                {
                    Monitor.Exit(sync);
                }
            }
        }

        public static T Data<T>(string cacheKey,
            System.Func<T> method)
        {
            return Data(cacheKey, CacheSeconds, method);
        }
        /// <summary>
        /// Executes a method and stores the result in cache using the given cache key.  If the data already exists in cache, it returns the data
        /// and doesn't execute the method.  Thread safe, although the method parameterisn't guaranteed to be thread safe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="cacheKey">Each method has it's own isolated set of cache items,so cacheKeys won't overlap accross methods.</param>
        /// <param name="method"></param>
        /// <param name="expirationSeconds">Lifetime of cache items, in seconds</param>
        /// <returns></returns>
        public static T Data<T>(string cacheKey,
            int expirationSeconds, System.Func<T> method)
        {
            if (!EnableCache) {
                return method();
            }
            EnsureHttpRuntime();
            var cache = HttpRuntime.Cache;
            var hash = method.GetHashCode().ToString();
            var data = (T)cache[hash + cacheKey];
            if (data == null)
            {
                data = method();

                if (expirationSeconds > 0 && data != null)
                    lock (sync)
                    {
                        cache.Insert(hash + cacheKey, data, null, DateTime.Now.AddSeconds
                            (expirationSeconds), Cache.NoSlidingExpiration);
                    }
            }
            return data;
        }
    }
}