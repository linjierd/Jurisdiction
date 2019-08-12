//-----------------------------------------------------------------------
// <copyright file="EntityContainer" company="lzh.com">
//     Copyright (c) lzh.com . All rights reserved.
// </copyright>
// <author>Zou Jian</author>
// <addtime>2010-10</addtime>
//-----------------------------------------------------------------------

namespace Permission.Library.DataTables.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Data;
    using System.Linq;
    using System.ComponentModel;
    using Permission.Library.Extensions;
    public class EntityContainer<T> where T : class
    {
        #region Private fields
        private readonly IEnumerable<T> _data;

        private readonly int _page;

        private readonly int _total;

        readonly bool _isHashKey;
        readonly string _idString;
     //   readonly Expression<Func<T, object>> _idExpression;
        #endregion

        #region Constructor

        static object DoProperyItem(Expression<Func<T, object>> dangerousCall, T item)
        {
            try
            {
                return dangerousCall.Compile().Invoke(item);
            }
            catch (Exception e)
            {
                Expression<Func<T, object>> dangerousExpression = dangerousCall;
                throw new Exception("错误的表达式：" + dangerousExpression, e);
            }
 
        }

        ///// <summary>
        ///// 生成Flexigrid可用的数据
        ///// </summary>
        ///// <param name="data">要转换的数据</param>
        ///// <param name="page">当前页面</param>
        ///// <param name="total">总条数</param>
        ///// <param name="identifier">主键</param>
        ///// <param name="properties">要添加的属性</param>
        //public EntityContainer(IEnumerable<T> data, int page, int total,
        //    Expression<Func<T, object>> identifier,
        //    Action<EntityPropertyContainer<T>> properties)
        //{
        //    if (data is IPagedList)
        //    {
        //        var pd = data as IPagedList;
        //        TotalString = pd.ExtString;
        //    }
        //    _data = data.ToList();
        //    _page = page;
        //    _total = total;
        //    _idExpression = identifier;
        //    Rows = new List<Entity>();
        //    // 运行主键委托
        //    var identityDelegate = identifier.Compile();
        //    // 获取属性集
        //    var dataCollection = new EntityPropertyContainer<T>();
        //    properties.Invoke(dataCollection);
        //    foreach (var item in _data)
        //    {
        //        var item1 = item;
        //        IList<string> rowData =
        //            dataCollection.ProperyValue
        //                .Select(properyItem => (DoProperyItem(properyItem,item1) ?? "").ToString()).ToList();
        //        // 创建DataList
        //        Rows.Add(new Entity(identityDelegate(item).ToString(), rowData));
        //    }
        //    Keys = dataCollection.ProperyKey;
        //}

        public EntityContainer(IEnumerable<T> data, int page, int total)
        {
            _data = data.ToList();
            if(data is IPagedList)
            {
                var pd = data as IPagedList;
                TotalString = pd.ExtString;
            }
            _page = page;
            _total = total;
            Rows = new List<Entity>();
            var propertyInfos = typeof(T).GetProperties();

            foreach (var item in _data)
            {
                var id = string.Empty;
                IList<string> cells = new List<string>();
                foreach (var info in propertyInfos)
                {
                    cells.Add((info.GetValue(item, null) ?? " ").ToString());
                    if (id.Length == 0)
                    {
                        _isHashKey = true;
                        id = item.GetHashCode().ToString();
                    }
                }
                Rows.Add(new Entity(id, cells));
            }
            Keys = propertyInfos.Select(c => c.Name).ToList();
        }

        public EntityContainer(DataTable data, int page, int total, string key)
        {
            _data = (data.AsEnumerable().Cast<T>()).ToList();
            _page = page;
            _total = total;
            _idString = key;
            Rows = new List<Entity>();
            Keys = data.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            foreach (DataRow item in data.Rows)
            {
                var k = key ?? "id";
                if (!Keys.Contains(k) && Keys.Count > 0)
                    k = Keys[0];
                var id = (item[k] ?? "").ToString();
                var values = item.ItemArray.Select(c => (c ?? "").ToString()).ToList();
                Rows.Add(new Entity(id, values));
            }
        }
        ///// <summary>
        ///// 初始化DataTable 并且追加自有属性
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="page"></param>
        ///// <param name="total"></param>
        ///// <param name="key"></param>
        ///// <param name="appendProperties"></param>
        //public EntityContainer(DataTable data, int page, int total,
        //    string key, Action<EntityPropertyContainer<T>> appendProperties) :
        //    this(data, page, total, key)
        //{
        //    Append(appendProperties);
        //    #region reg

        //    //var dataCollection = new EntityPropertyContainer<T>();
        //    //appendProperties.Invoke(dataCollection);
        //    //foreach (DataRow item in data.Rows)
        //    //{
        //    //    var item1 = item;
        //    //    IList<string> rowData =
        //    //        dataCollection.ProperyValue
        //    //        .Select(properyItem => properyItem(item1 as T).ToString()).ToList();
        //    //    // 创建DataList
        //    //    var row = _rows.FirstOrDefault(c => c.id == item[key].ToString());
        //    //    if (row != null){
        //    //        rowData.ForEach(c =>
        //    //        {
        //    //            row.cell.Add(c);
        //    //        });
        //    //    }
        //    //}
        //    //dataCollection.ProperyKey.ForEach(c =>
        //    //{
        //    //    _keys.Add(c);
        //    //});
        //    #endregion
        //}



        #endregion

        #region Append

        public EntityContainer<T> Append(Action<EntityPropertyContainer<T>> appendProperties)
        {
            var dataCollection = new EntityPropertyContainer<T>();
            appendProperties.Invoke(dataCollection);
            foreach (var item in _data)
            {
                var item1 = item;
                IList<string> rowData =
                    dataCollection.ProperyValue
                        .Select(properyItem => (DoProperyItem(properyItem, item1) ?? "").ToString()).ToList();

                // 创建DataList
                var key = GetItemKey(item);
                var row = Rows.FirstOrDefault(c => c.Id == key);
                if (row != null)
                {
                    rowData.ForEach(c => row.Cell.Add(c));
                }
            }
            dataCollection.ProperyKey.ForEach(c => Keys.Add(c));
            return this;
        }
        #region remove

        public EntityContainer<T> RemoveParams(params Expression<Func<T, object>>[] keys)
        {
            foreach (var key in keys)
            {
                Remove(key);
            }
            return this;
        }

        public EntityContainer<T> Remove(Expression<Func<T, object>> key)
        {
            var m = (key.Body.RemoveUnary() as MemberExpression);
            if (m != null)
            {
                return Remove(m.Member.Name);
            }
            return this;
        }

        public EntityContainer<T> Remove(string key)
        {
            var index = Keys.IndexOf(key);
            if (index > 0)
            {
                Keys.RemoveAt(index);
                Rows.ForEach(c => c.Cell.RemoveAt(index));
            }
            return this;
        }
        #endregion
        private string GetItemKey(T item)
        {
            if (_isHashKey)
            {
                return item.GetHashCode().ToString();
            }
            //if (_idExpression != null)
            //{
            //    var identityDelegate = _idExpression.Compile();
            //    return identityDelegate(item).ToString();
            //}
            var row = item as DataRow;
            if (!string.IsNullOrEmpty(_idString) && row != null)
            {
                return row[_idString].ToString();
            }
            throw new Exception("追加时Key关系生成不成功");
        }
        #endregion

        #region prop
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Page { get { return _page; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Total { get { return _total; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IList<Entity> Rows { get; private set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IList<string> Keys { get; private set; }
        public string TotalString { get; set; }
        #endregion
    }
}