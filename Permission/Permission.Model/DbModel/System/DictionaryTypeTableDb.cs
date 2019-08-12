using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Model.DbModel.System
{
    [Table("dictionary_type_table")]
    public class DictionaryTypeTableDb
    {
        public DictionaryTypeTableDb()
        { }
        #region Model
        /// <summary>
        /// 字典类别key
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "字典类别key")]
        public string dt_type_key
        {
            get; set;
        }
        /// <summary>
        /// 字典类别名称
        /// </summary>
        [Required]
        [Display(Name = "字典类别名称")]
        public string dt_type_name
        {
            get; set;
        }
        /// <summary>
        /// 字典类别备注
        /// </summary>
        [Required]
        [Display(Name = "字典类别备注")]
        public string dt_type_remark
        {
            get; set;
        }
        /// <summary>
        /// 字典类别排序
        /// </summary>
        [Required]
        [Display(Name = "排序")]
        public decimal dt_type_orderby
        {
            get; set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [Display(Name = "创建人")]
        public string creator_name
        {
            get; set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Display(Name = "创建时间")]
        public DateTime creator_date
        {
            get; set;
        }
        /// <summary>
        /// 修改人
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

       
        #endregion Model
    }
}
