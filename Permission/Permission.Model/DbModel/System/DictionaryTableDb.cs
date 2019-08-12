using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Model.DbModel.System
{
    /// <summary>
    /// 实体类 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("dictionary_table")]
    public class DictionaryTableDb
    {
        public DictionaryTableDb()
        { }
        #region Model
        /// <summary>
        /// 字典key
        /// </summary>
        [Key]
        [Display(Name = "字典key")]
        public string dt_key
        {
            get; set;
        }
        /// <summary>
        /// 字典类别key
        /// </summary>
        [Display(Name = "字典类别key")]
        public string dt_type_key
        {
            get; set;
        }
        /// <summary>
        /// 字典名称
        /// </summary>
        [Display(Name = "字典名称")]
        public string dt_name
        {
            get; set;
        }
        /// <summary>
        /// 字典状态 1:有效  2:无效
        /// </summary>
        [Display(Name = "字典状态")]
        public int dt_status
        {
            get; set;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Display(Name = "排序")]
        public decimal dt_orderby
        {
            get; set;
        }
        /// <summary>
        /// 创建人登陆名
        /// </summary>
        [Display(Name = "创建人")]
        public string creator_name
        {
            get; set;
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime creator_date
        {
            get; set;
        }
        /// <summary>
        /// 修改人登录名
        /// </summary>
        [Display(Name = "修改人")]
        public string modifi_name
        {
            get; set;
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Display(Name = "修改日期")]
        public DateTime? modifi_date
        {
            get; set;
        }

        public virtual DictionaryTypeTableDb DictionaryTypeTableDb { get; set; }
        #endregion Model


    }
}
