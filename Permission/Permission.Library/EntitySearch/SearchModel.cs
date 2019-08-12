
namespace Permission.Library {
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Extensions;
    using Tools.DataTimeTools;


    /// <summary>
    /// 用户自动收集搜索条件的Model
    /// </summary>
    public class SearchModel
    {
        #region Ctor

        public SearchModel()
        {
            Items = new List<SearchItem>(); length = 10; start = 0;
            ParamList = new List<object>();
        }

        #endregion

        #region 基础属性 用于分页、排序以及查询


    

        /// <summary>
        /// 要排序的字段
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序顺序 
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// 开始记录
        /// </summary>
        public int start { get; set; }
        /// <summary>
        /// 页长
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// 客户端标识
        /// </summary>
        public int draw { get; set; }


        /// <summary>
        /// 查询条件
        /// </summary>
        public List<SearchItem> Items { get; set; }

        #endregion

        #region 生成SQL及参数的方法

        /// <summary>
        /// 如果有查询则生成需要的标准SQL
        /// </summary>
        /// <returns></returns>
        public string ToSearchString()
        {
            return ToSearchStringAbstract(GetSearchString);
        }
        /// <summary>
        /// 如果有查询则生成需要的标准SQL,不带参数变量
        /// Add by guoqi.zhao
        /// </summary>
        /// <returns></returns>
        public string ToNoParamsSearchString()
        {
            return ToSearchStringAbstract(GetNoParamsSearchString);
        }
        public List<object> ParamList { get; set; }
        public string ToSearchStringAbstract(Action<string, IEnumerable<SearchItem>, StringBuilder> func)
        {
            ParamList.Clear();
            var sb = new StringBuilder();
            var andItems = Items.Where(c => string.IsNullOrEmpty(c.OrGroup)).ToList();
            func("AND", andItems, sb);
            var orItems = Items.Where(c => !string.IsNullOrEmpty(c.OrGroup)).ToList();
            //var nowIndex = andItems.Count(c => c.Method != SearchMethod.Like && c.Method != SearchMethod.In && c.Method != SearchMethod.NotIn);
            foreach (var searchItem in orItems.GroupBy(c => c.OrGroup))
            {
                sb.Append(" (");
                func("OR", searchItem, sb);
                sb.Remove(sb.Length - 2, 2);
                sb.Append(") AND");
                //nowIndex += searchItem.Count();
            }

            return sb.ToString().TrimEnd("AND".ToCharArray());
        }

        private void GetSearchString(string opr, IEnumerable<SearchItem> andItems, StringBuilder sb)//, int index
        {
            var list = andItems.ToList();
            //int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item.Method == SearchMethod.In || item.Method == SearchMethod.NotIn)
                {
                    //当为In时要有一个() hujingang 2010-9-17 15:50:26
                    sb.AppendFormat(" {0} {1} ({2}) " + opr, item.Field, item.Method.GetGlobalCode(), item.GetInValue(item.Value));
                }
                else
                {
                    sb.AppendFormat(" {0} {1} @p{2} " + opr, item.Field, item.Method.GetGlobalCode(), ParamList.Count);
                    // index++;
                    ParamList.Add(item.Value);

                }
                if (item.Method == SearchMethod.Like && !item.Value.ToString().Contains("%"))
                {
                    item.Value = string.Format("%{0}%", item.Value);
                }
            }
        }
       


        private void GetNoParamsSearchString(string optr, IEnumerable<SearchItem> andItems, StringBuilder sb)
        {
            var list = andItems.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];// hujingang 2010-11-29 17:22:40
                if (item.Method == SearchMethod.In || item.Method == SearchMethod.NotIn)
                {
                    sb.AppendFormat(" {0} {1} ({2}) " + optr, item.Field, item.Method.GetGlobalCode(), item.GetInValue(item.Value));
                }
                //else if (item.Method == SearchMethod.Like)
                //{
                //    sb.AppendFormat(" {0} {1} '%{2}%' " + optr, item.Field, item.Method.GetGlobalCode(), item.Value);
                //}
                else
                {
                    sb.AppendFormat(" {0} {1} '{2}' " + optr, item.Field, item.Method.GetGlobalCode(), item.Value);
                }
                if (item.Method == SearchMethod.Like && !item.Value.ToString().Contains("%"))
                {
                    item.Value = string.Format("%{0}%", item.Value);
                }
            }
        }

        /// <summary>
        /// 根据查询条件生成带Where的SQL
        /// added by Li Bo 2010-09-15
        /// </summary>
        /// <returns></returns>
        public string ToWhereSearchString(bool hasParam = true)
        {
            string sqlStr = ToSearchString();
            return string.IsNullOrEmpty(sqlStr) ? "" : " WHERE " + (hasParam ? sqlStr : ToNoParamsSearchString());
        }

        /// <summary>
        /// 生成排序的SQL
        /// added by Li Bo 2010-09-15
        /// edit by Tian Jing 2010-10-13
        /// </summary>
        /// <returns></returns>
        public string ToOrderString()
        {
            return ToOrderString(SortName, SortOrder);
        }

        /// <summary>
        /// 生成排序
        /// added by Tian Jing 2010-10-13
        /// edit by Li Bo 2010-12-22
        /// </summary>
        /// <returns></returns>
        public static string ToOrderString(string sortName, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortName))
                return "";

            return string.Format(" ORDER BY {0} {1} ", sortName, sortOrder);
        }

        public string ToPagerString()
        {
            return ToPagerString(start, length);
        }

        public string ToPagerString(string sql)
        {
            return GetPageSql(sql, ToOrderString(), length, start);
        }
        public string GetPageSql(string sql, string orderField, int start, int length)
        {
            string StrSql = " ";
            int StartRecord = 0, EndRecord = 0;

           
            StartRecord = start;
            EndRecord = StartRecord + length ;
            StrSql = "     select * from (   SELECT ROW_NUMBER() Over( " + orderField + " ) as rowId,* from( " +
                     sql + " ) AS a) as t WHERE rowId BETWEEN  " + StartRecord.ToString() + " and " +
                     EndRecord.ToString() + " order by rowId ";
            return StrSql;

        }

        /// <summary>
        /// 生成分页limit
        /// added by Tian Jing 2010-10-9
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static string ToPagerString(int page, int pageSize)
        {
            return string.Format(" limit {0},{1}", (page - 1) * pageSize, pageSize);
        }

        /// <summary>
        /// [zj]
        /// 使用ExecuteStoreCommand等需要传入参数进行查询的方法时，对应的参数列表
        /// </summary>
        /// <returns></returns>
        public object[] GetObjectParmaters()
        {
            return ParamList.ToArray();
            // return Items.Where(x => x.Method != SearchMethod.In).Select(c => c.Value).ToArray();
        }

        #endregion

        #region Decorate SearchModel

        /// <summary>
        /// 将指定fields中value为DateTime格式的值转为unix_time。若DateTime时间为00:00:00，则LessThan的时间自动加上23:59:59
        /// </summary>
        /// <param name="toUnixtime">是否将时间转为UnixtimeStamp.否则转为"yyyy-MM-dd HH:mm:ss"格式的字符串</param>
        /// <param name="fields">要修改的字段名称</param>
        /// <returns></returns>
        public SearchModel DecorateDateTime(bool toUnixtime, params string[] fields)
        {
            if (this != null && this.Items != null && fields != null) //不为空
            {
                //遍历所有要修改的字段
                this.Items.FindAll(t => fields.Contains(t.Field)).ForEach(e =>
                {
                    DateTime value = new DateTime();
                    if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out value))
                    {
                        //只设置了日期没有时间,截止日期加上23;59:59
                        if (value.Date == value && (e.Method == SearchMethod.LessThan || e.Method == SearchMethod.LessThanOrEqual))
                        {
                            value = value.AddDays(1).AddSeconds(-1);
                        }

                        if (toUnixtime)
                            e.Value = UnixTime.FromDateTime(value);
                        else
                            e.Value = value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                });
            }

            return this;
        }

        #endregion

    }
}