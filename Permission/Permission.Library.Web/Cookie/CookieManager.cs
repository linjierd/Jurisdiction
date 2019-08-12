using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Permission.Library.Web.Cookie
{
    public class CookieManager
    {
        /// <summary>
        /// 写入cookie
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <param name="value"></param>
        public static void SetCookie(string cookieKey, string value)
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieKey];
            if (null == cookie)
            {
                cookie = new HttpCookie(cookieKey);
            }
            cookie.Value = value;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieKey)
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieKey];
            if (null != cookie)
            {
                return cookie.Value;
            }
            return "";
        }
    }
}
