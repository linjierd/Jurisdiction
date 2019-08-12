namespace Permission.Library.Extensions
{
    using EntitySearch;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///   Linq IQueryable 扩展
    /// </summary>
    public static class IQueryableExtensions
    {
        #region OrderBy

        /// <summary>
        ///   扩展OrderBy方法，使之支持字符串
        ///   LzhPopedom.Library.Framework.Systems.IQueryableExtensions
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "query"></param>
        /// <param name = "sortName"></param>
        /// <param name = "sortOrder"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortName, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortName)) return query;
            //if (property == null) return query;
            var param = Expression.Parameter(typeof (T), "o");

            var props = sortName.Split('.');
            Expression propertyAccess = param;
            var typeOfProp = typeof (T);
            var i = 0;
            do
            {
                var property = typeOfProp.GetProperty(props[i]);
                if (property == null) throw new Exception("OrderBy方法,可能位于Where(SearchModel)中，或不到所指定的属性:" + sortName);
                typeOfProp = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                i++;
            } while (i < props.Length);

            var orderByExp = Expression.Lambda(propertyAccess, param);
            var resultExp =
                Expression.Call(
                    typeof (Queryable),
                    sortOrder == "desc" ? "OrderByDescending" : "OrderBy",
                    new[] {typeof (T), typeOfProp},
                    query.Expression,
                    Expression.Quote(orderByExp));
            return query.Provider.CreateQuery<T>(resultExp);
        }

        #endregion

        #region Where in

        /// <summary>
        ///   使之支持Sql in语法
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <typeparam name = "TValue"></typeparam>
        /// <param name = "query"></param>
        /// <param name = "obj"></param>
        /// <param name = "values"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIn<T, TValue>(this IQueryable<T> query, Expression<Func<T, TValue>> obj, IEnumerable<TValue> values)
        {
            return query.Where(BuildContainsExpression(obj, values));
        }

        private static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
            Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector)
            {
                throw new ArgumentNullException("valueSelector");
            }
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }
            var p = valueSelector.Parameters.Single();
            if (!values.Any()) return e => false;

            var equals = values.Select(value => (Expression) Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof (TValue))));
            var body = equals.Aggregate(Expression.Or);
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        #endregion

        #region Where

        /// <summary>
        ///   zoujian add , 使IQueryable支持SearchModel
        /// </summary>
        /// <typeparam name = "TEntity"></typeparam>
        /// <param name = "table">IQueryable的查询对象</param>
        /// <param name = "model">SearchModel对象</param>
        /// <param name = "prefix">使用前缀区分查询条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, SearchModel model, string prefix = "") where TEntity : class
        {
            Contract.Requires(table != null);
            if(!string.IsNullOrEmpty(prefix))
            {
                return new QueryableSearcher<TEntity>(table, model.Items.Where(c => c.Prefix == prefix)).Search();
            }
            return new QueryableSearcher<TEntity>(table, model.Items.Where(c => string.IsNullOrEmpty(c.Prefix))).Search();

        }

        /// <summary>
        ///   支持SearchItem查询
        /// </summary>
        /// <typeparam name = "TEntity"></typeparam>
        /// <param name = "table">IQueryable的查询对象</param>
        /// <param name = "item">SearchItem</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, SearchItem item)
        {
            Contract.Requires(item != null);
            Contract.Requires(!string.IsNullOrEmpty(item.Field));
            return new QueryableSearcher<TEntity>(table, new List<SearchItem> {item}).Search();
        }

        /// <summary>
        ///   支持对IQueryable通过字符串指定属性的查询
        /// </summary>
        /// <typeparam name = "TEntity"></typeparam>
        /// <param name = "table">IQueryable的查询对象</param>
        /// <param name = "field">属性名</param>
        /// <param name = "method">判断谓词</param>
        /// <param name = "value">值</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, string field,
                                                         SearchMethod method, object value)
        {
            return table.Where(new SearchItem {Field = field, Method = method, Value = value});
        }

        #endregion

        #region Obsolete

        [Obsolete("请使用Where方法，并设置SearchItem的OrGroup", true)]
        public static IQueryable<TEntity> WhereOr<TEntity>(IQueryable<TEntity> ret, IEnumerable<SearchItem> entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}