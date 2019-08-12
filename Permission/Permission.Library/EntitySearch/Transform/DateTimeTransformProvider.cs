namespace Permission.Library.EntitySearch.Transform
{
    using System;
    using System.Collections.Generic;
    using Tools.TypeTools;

    public class DateTimeTransformProvider : ITransformProvider
    {
        #region ITransformProvider Members

        public bool Match(SearchItem item, Type type)
        {
            var realType = TypeUtil.GetUnNullableType(type);
            return realType == typeof (DateTime)
                   && !(item.Value is DateTime)
                   && (item.Method == SearchMethod.LessThan || item.Method == SearchMethod.LessThanOrEqual);
        }

        public IEnumerable<SearchItem> Transform(SearchItem item, Type type)
        {
            DateTime willTime;
            DateTime.TryParse(item.Value.ToString(), out willTime);
            if (willTime.Hour == 0 && willTime.Minute == 0 && willTime.Second == 0)
            {
                willTime = willTime.AddDays(1).AddMilliseconds(-1);
            }
            return new[] {new SearchItem(item.Field, SearchMethod.LessThanOrEqual, willTime)};
        }

        #endregion
    }
}