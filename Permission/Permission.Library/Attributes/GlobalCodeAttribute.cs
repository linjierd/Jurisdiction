using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Permission.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class GlobalCodeAttribute : DescriptionAttribute
    {

        public GlobalCodeAttribute(string description) : base(description) { }

        /// <summary>
        /// 如将此属性设置为true，则此描述不保存在数据库中
        /// </summary>
        public bool OnlyAttribute { get; set; }

        /// <summary>
        /// 如果显示时允许显示颜色，则需要预先定义此属性
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 显示顺序，数字小的排在前面
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string ExtName { get; set; }
    }
}
