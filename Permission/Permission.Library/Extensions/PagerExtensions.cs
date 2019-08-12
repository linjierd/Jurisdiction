using System;
using System.Collections.Generic;
using System.Linq;

namespace Permission.Library.Extensions
{
    public static class PagerExtensions
    {
        const int DefaultLength = 20;
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static PagedList<T> Pager<T>(this  IQueryable<T> source, int start, int length)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException("start",
                                                      @"当前参数start必须>=0.");
            return new PagedList<T>(source, start, length);
        }

        public static PagedList<T> Pager<T>(this  IQueryable<T> source, int start)
        {
            return source.Pager(start, DefaultLength);
        }
        public static PagedList<T> Pager<T>(this  IEnumerable<T> source, int start, int length)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException("start",
                                                      @"当前参数currentPage必须>=0.");
            return new PagedList<T>(source, start, length);
        }
        public static PagedList<T> Pager<T>(this  IEnumerable<T> source, int start)
        {
            return source.Pager(start, DefaultLength);
        }
        /// <summary>
        /// 对数据进行分页操作，并按参数进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static PagedList<T> Pager<T>(this IQueryable<T> query, SearchModel m)
        {
            if (string.IsNullOrEmpty(m.SortName))
                throw new Exception("请设置Flexigrid的DefaultSortOption方法，在调用Pager前对调用OrderBy方法");
            return query.OrderBy(m.SortName, m.SortOrder).Pager(m.start, m.length);
        }
    }
}
