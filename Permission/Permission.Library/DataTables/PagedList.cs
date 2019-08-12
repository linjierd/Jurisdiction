using System;
using System.Collections.Generic;
using System.Linq;

namespace Permission.Library
{
    using System.ComponentModel;

    /// <summary>
    /// 可以对IQueryable或IEnumerable进行分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList(IEnumerable<T> content, int currentPage, int pageSize, int totalCount, int pageType = 2)
            : this(totalCount, currentPage, pageSize, pageType)
        {
            AddRange(content);
        }
        public PagedList(IQueryable<T> source, int currentPage, int pageSize, int pageType = 2)
            : this(source.Count(), currentPage, pageSize, pageType)
        {
            if (pageType == 2) AddRange(source.Skip(Start).Take(Length).ToList());

            AddRange(source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList());
        }

        public PagedList(IEnumerable<T> source, int currentPage, int pageSize, int pageType = 2)
            : this(source.Count(), currentPage, pageSize, pageType)
        {
            if (pageType == 2) AddRange(source.Skip(Start).Take(Length).ToList());
            AddRange(source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">页总数</param>
        /// <param name="currentPage">当前页或者起始记录</param>
        /// <param name="pageSize">页长</param>
        /// <param name="pageType">分页类型,1:按起始记录来分 2:按当前页来分</param>
        protected PagedList(int count, int currentPage, int pageSize, int pageType = 2)
        {
            if (pageType == 1)
            {
                TotalCount = count;
                PageSize = Math.Max(pageSize, 1);
                CurrentPage = Math.Min(Math.Max(currentPage, 1), TotalPages);
            }
            if (pageType == 2)
            {
                TotalCount = count;
                Length = Math.Max(pageSize, 1);
                Start = currentPage;
            }

        }


        //public PagedList(IEnumerable<T> content, int start, int length, int recordsFiltered)
        //    : this(start, length, recordsFiltered)
        //{
        //    AddRange(content);
        //}
        //public PagedList(IQueryable<T> source, int start, int length)
        //    : this(source.Count(), start, length)
        //{
        //    AddRange(source.Skip(start).Take(length).ToList());
        //}

        //public PagedList(IEnumerable<T> source, int start, int length)
        //    : this(source.Count(), start, length)
        //{
        //    AddRange(source.Skip(start).Take(length).ToList());
        //}
        //protected PagedListDataTables(int count, int start, int length,int pageType)
        //{
        //    RecordsFiltered = count;
        //    Length = Math.Max(length, 1);
        //    Start = start;
        //}
        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页数据量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return Math.Max((TotalCount + PageSize - 1) / PageSize, 1); }
        }
        /// <summary>
        /// 当前页号
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 每页数据量
        /// </summary>
        public int Length { get; set; }





        /// <summary>
        /// 扩展文本
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ExtString { get; set; }


    }
}