
using Permission.Library;
using Permission.Library.Extensions;
using System.Linq;
using System.Text;

namespace Permission.Library
{

    /// <summary>
    /// 用于存储查询条件的单元
    /// </summary>
    public class SearchItem
    {
        public SearchItem() { }

        public SearchItem(string field, SearchMethod method, object val)
        {
            Field = field;
            Method = method;
            Value = val;
        }

        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 查询方式，用于标记查询方式HtmlName中使用[]进行标识
        /// </summary>
        public SearchMethod Method { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 前缀，用于标记作用域，HTMLName中使用()进行标识
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 如果使用Or组合，则此组组合为一个Or序列
        /// </summary>
        public string OrGroup { get; set; }

        public string GetSearchString(bool isParams = false, int paramsIndex = 0)
        {
            // StringBuilder sb = new StringBuilder();
            string searchString = "";
            string paramField = "";
            if (isParams)
            {
                paramField = string.Concat("@p", paramsIndex);
            }
            if (Method == SearchMethod.Like && !Value.ToString().Contains("%"))
            {
                Value = string.Format("%{0}%", Value);
            }
            if (Method == SearchMethod.In || Method == SearchMethod.NotIn)
            {
                searchString = string.Format(" {0} {1} ({2}) ", Field, Method.GetGlobalCode(), GetInValue(Value));
            }
            else
            {
                if (isParams)
                {
                    searchString = string.Format("  {0} {1} {2} ", Field, Method.GetGlobalCode(), paramField);
                }
                else
                {
                    searchString = string.Format("   {0} {1} '{2}' ", Field, Method.GetGlobalCode(), Value);
                }
            }
            return searchString;
        }

        public  object GetInValue(object itemvalue)
        {
            if (itemvalue is int[])
            {
                return string.Join(",", (itemvalue as int[]).Select(x => x.ToString()));
            }
            if (itemvalue is long[])
            {
                return string.Join(",", (itemvalue as long[]).Select(x => x.ToString()));
            }
            return itemvalue;
        }
    }
}
