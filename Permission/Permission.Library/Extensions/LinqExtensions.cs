using System;
using System.Collections;
using System.Collections.Generic;

namespace Permission.Library.Extensions
{
    static public class LinqExtensions
    {
        /// <summary>
        /// 存在 List&lt;T&gt;.ForEach但是其它的IEnumerable不存在ForEach方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            if (enumeration != null)
                foreach (var item in enumeration)
                    action(item);
        }
        /// <summary>
        /// 存在 List&lt;T&gt;.ForEach但是其它的IEnumerable不存在ForEach方法
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach(this IEnumerable enumeration, Action<object> action)
        {
            if (enumeration != null)
                foreach (var item in enumeration)
                    action(item);
        }

    }
}