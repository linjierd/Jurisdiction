

namespace Permission.Library
{
    using DataTables.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DataTablesExtensions
    {
   

        /// <summary>
        ///   生成DataTables所需的对象
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "list"></param>
        /// <returns></returns>
        public static EntityContainer<T> ToDataTablesObject<T>(this PagedList<T> list) where T : class
        {
            var json = new EntityContainer<T>(
                list, list.CurrentPage,
                list.TotalCount);
            return json;
        }



        public static EntityContainer<T> ToDataTablesObject<T>(this IEnumerable<T> list) where T : class
        {
            var json = new EntityContainer<T>(
                list, 1,
                list.Count());
            return json;
        }

    }
}