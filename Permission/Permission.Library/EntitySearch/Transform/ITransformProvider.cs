namespace Permission.Library.EntitySearch.Transform
{
    using System;
    using System.Collections.Generic;

    public interface ITransformProvider
    {
        bool Match(SearchItem item, Type type);
        IEnumerable<SearchItem> Transform(SearchItem item, Type type);
    }
}