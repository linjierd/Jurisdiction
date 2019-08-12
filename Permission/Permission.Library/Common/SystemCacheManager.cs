using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Permission.Library.Common
{
    public static class SystemCacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeOut"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key, int timeOut, Func<T> function)
        {
            if (string.IsNullOrEmpty(key)) return default(T);
            var configs = HttpContext.Current.Cache[key];
            if (configs == null)
            {
                configs = function();
                if (timeOut > -1)
                {
                    HttpContext.Current.Cache.Insert(key, configs,
                                                null, DateTime.Now.AddSeconds(timeOut),
                                                System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    HttpContext.Current.Cache.Insert(key, configs);
                }
                return (T)configs;
            }
            return (T)configs;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeOut"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static T GetCache<T,Z>(string key, int timeOut, Func<Z,T> function,Z u1)
        {
            if (string.IsNullOrEmpty(key)) return default(T);
            var configs = HttpContext.Current.Cache[key];
            if (configs == null)
            {
                configs = function(u1);
                if (timeOut > -1)
                {
                    HttpContext.Current.Cache.Insert(key, configs,
                                                null, DateTime.Now.AddSeconds(timeOut),
                                                System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    HttpContext.Current.Cache.Insert(key, configs);
                }
                return (T)configs;
            }
            return (T)configs;
        }
    }
}
