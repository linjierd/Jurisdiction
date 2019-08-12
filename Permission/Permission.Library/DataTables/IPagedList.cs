using System.Collections;

namespace Permission.Library
{
    /// <summary>
    /// �ɷ�ҳ�Ľӿ�
    /// </summary>
    public interface IPagedList : IEnumerable
    {
        int TotalPages { get; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        string ExtString { get; set; }
    }
}