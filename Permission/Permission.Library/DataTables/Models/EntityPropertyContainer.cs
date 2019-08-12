//-----------------------------------------------------------------------
// <copyright file="EntityPropertyContainer" company="lzh.com">
//     Copyright (c) lzh.com . All rights reserved.
// </copyright>
// <author>Zou Jian</author>
// <addtime>2010-10</addtime>
//-----------------------------------------------------------------------

//-----------------------------------------------------------------------
// <copyright file="EntityPropertyContainer" company="lzh.com">
//     Copyright (c) lzh.com . All rights reserved.
// </copyright>
// <author>Zou Jian</author>
// <addtime>2010-11</addtime>
//-----------------------------------------------------------------------

namespace Permission.Library.DataTables.Models
{
    using Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// 用于序列化Flex json object 的属性集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityPropertyContainer<T> where T : class
    {
        public EntityPropertyContainer()
        {
            ProperyValue = new List<Expression<Func<T, object>>>();
            ProperyKey = new List<string>();
        }

        internal IList<Expression<Func<T, object>>> ProperyValue { get; set; }
        internal IList<string> ProperyKey { get; set; }
        public EntityPropertyContainer<T> Add(Expression<Func<T, object>> value)
        {
            var m = (value.Body.RemoveUnary() as MemberExpression);
            if (m != null)
            {
                ProperyKey.Add(m.Member.Name);
                ProperyValue.Add(value);
            }
            return this;
        }
        public EntityPropertyContainer<T> Add(Expression<Func<T, object>> key, Expression<Func<T, object>> value)
        {
            var m = (key.Body.RemoveUnary() as MemberExpression);
            if (m != null)
            {
                ProperyKey.Add(m.Member.Name);
                ProperyValue.Add(value);
            }
            return this;
        }
        public EntityPropertyContainer<T> Add(string key, Expression<Func<T, object>> value)
        {
            ProperyKey.Add(key);
            ProperyValue.Add(value);
            return this;
        }


       
    }
}