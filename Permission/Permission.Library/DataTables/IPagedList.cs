using System.Collections;

namespace Permission.Library
{
    /// <summary>
    /// 可分页的接口
    /// </summary>
    public interface IPagedList : IEnumerable
    {
        int TotalPages { get; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        string ExtString { get; set; }
    }
}