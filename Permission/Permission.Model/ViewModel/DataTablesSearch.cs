using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Model.ViewModel
{
    /// <summary>
    /// DataTables查询模型
    /// </summary>
    public class DataTablesSearch
    {
        /// <summary>
        /// 客户端与服务端的沟通标识
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 起始记录
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 页长度
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string sort_name { get; set; }

        public string sort_order { get; set; }

        public Dictionary<string, string> sort_order_list { get; set; }

        public Dictionary<string, string> post_data { get; set; }
    }
}
