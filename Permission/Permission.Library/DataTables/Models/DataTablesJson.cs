using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permission.Library
{
    public class DataTablesJson
    {
        /// <summary>
        /// 客户端Key,DataTables专用
        /// </summary>
        public int draw { get; set; }
        /// <summary>
        /// 所有记录总数
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int recordsTotal { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<Dictionary<string, object>> data { get; set; }
    }
}
