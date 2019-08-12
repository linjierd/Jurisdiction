using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Permission.WebManager.Lib.WebModel.SystemManager
{
    public class DictionaryWebM
    {
        #region Model
        /// <summary>
        /// 字典key
        /// </summary>
        [Required]
        [Display(Name = "字典key")]
        [Remote("ValidateKey", "DictionaryManager")]
        public string dt_key
        {
            get; set;
        }
        /// <summary>
        /// 字典类别key
        /// </summary>
        [Required]
        [Display(Name = "字典类别")]
        public string dt_type_key
        {
            get; set;
        }
        /// <summary>
        /// 字典名称
        /// </summary>
        [Required]
        [Display(Name = "字典名称")]
        public string dt_name
        {
            get; set;
        }
        /// <summary>
        /// 字典状态 1:有效  2:无效
        /// </summary>
        [Required]
        [Display(Name = "是否有效")]
        public int dt_status
        {
            get; set;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Required]
        [RegularExpression(@"^((-?)0|([1-9][0-9]*))(\.[0-9]+)?$", ErrorMessage = "排序必须是数字")]
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
        #endregion Model
    }
}
