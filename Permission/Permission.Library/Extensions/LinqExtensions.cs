using System;
using System.Collections;
using System.Collections.Generic;

namespace Permission.Library.Extensions
{
    static public class LinqExtensions
    {
        /// <summary>
        /// ���� List&lt;T&gt;.ForEach����������IEnumerable������ForEach����
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
        /// ���� List&lt;T&gt;.ForEach����������IEnumerable������ForEach����
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