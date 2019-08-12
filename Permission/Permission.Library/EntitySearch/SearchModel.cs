
namespace Permission.Library {
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Extensions;
    using Tools.DataTimeTools;


    /// <summary>
    /// �û��Զ��ռ�����������Model
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

        #region �������� ���ڷ�ҳ�������Լ���ѯ


    

        /// <summary>
        /// Ҫ������ֶ�
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// ����˳�� 
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// ��ʼ��¼
        /// </summary>
        public int start { get; set; }
        /// <summary>
        /// ҳ��
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// �ͻ��˱�ʶ
        /// </summary>
        public int draw { get; set; }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        public List<SearchItem> Items { get; set; }

        #endregion

        #region ����SQL�������ķ���

        /// <summary>
        /// ����в�ѯ��������Ҫ�ı�׼SQL
        /// </summary>
        /// <returns></returns>
        public string ToSearchString()
        {
            return ToSearchStringAbstract(GetSearchString);
        }
        /// <summary>
        /// ����в�ѯ��������Ҫ�ı�׼SQL,������������
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
                    //��ΪInʱҪ��һ��() hujingang 2010-9-17 15:50:26
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
        /// ���ݲ�ѯ�������ɴ�Where��SQL
        /// added by Li Bo 2010-09-15
        /// </summary>
        /// <returns></returns>
        public string ToWhereSearchString(bool hasParam = true)
        {
            string sqlStr = ToSearchString();
            return string.IsNullOrEmpty(sqlStr) ? "" : " WHERE " + (hasParam ? sqlStr : ToNoParamsSearchString());
        }

        /// <summary>
        /// ���������SQL
        /// added by Li Bo 2010-09-15
        /// edit by Tian Jing 2010-10-13
        /// </summary>
        /// <returns></returns>
        public string ToOrderString()
        {
            return ToOrderString(SortName, SortOrder);
        }

        /// <summary>
        /// ��������
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
        /// ���ɷ�ҳlimit
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
        /// ʹ��ExecuteStoreCommand����Ҫ����������в�ѯ�ķ���ʱ����Ӧ�Ĳ����б�
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
        /// ��ָ��fields��valueΪDateTime��ʽ��ֵתΪunix_time����DateTimeʱ��Ϊ00:00:00����LessThan��ʱ���Զ�����23:59:59
        /// </summary>
        /// <param name="toUnixtime">�Ƿ�ʱ��תΪUnixtimeStamp.����תΪ"yyyy-MM-dd HH:mm:ss"��ʽ���ַ���</param>
        /// <param name="fields">Ҫ�޸ĵ��ֶ�����</param>
        /// <returns></returns>
        public SearchModel DecorateDateTime(bool toUnixtime, params string[] fields)
        {
            if (this != null && this.Items != null && fields != null) //��Ϊ��
            {
                //��������Ҫ�޸ĵ��ֶ�
                this.Items.FindAll(t => fields.Contains(t.Field)).ForEach(e =>
                {
                    DateTime value = new DateTime();
                    if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out value))
                    {
                        //ֻ����������û��ʱ��,��ֹ���ڼ���23;59:59
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