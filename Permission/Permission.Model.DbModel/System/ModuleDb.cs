using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Permission.Model.DbModel.System
{
    [Table("module_db")]
    public class ModuleDb 
    {
        #region Model
        /// <summary>
        /// 功能code
        /// </summary>
        [Key]
        [Required]
        [StringLength(50)]
        public string module_code
        {
            get;
            set;
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string module_name
        {
            get;
            set;
        }
        /// <summary>
        /// 父cdoe
        /// </summary>
        [Required]
        [StringLength(50)]
        public string parent_code
        {
            get;
            set;
        }
        /// <summary>
        /// 功能级别
        /// </summary>
         [Required]
        public int module_level
        {
            get;
            set;
        }
        /// <summary>
        /// 是否菜单
        /// </summary>
         [Required]
        public int is_menu
        {
            get;
            set;
        }
        /// <summary>
        /// 是否请求
        /// </summary>
         [Required]
         public int is_action { get; set; }

         /// <summary>
         /// 请求url
         /// </summary>
         public string action_url { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
         [Required]
         public decimal order_by { get; set; }
         public int module_status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
         [StringLength(50)]
        public string creator_name
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人名称
        /// </summary>
         [StringLength(100)]
        public string creator_full_name
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creator_time
        {
            get;
            set;
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modifi_name
        {
            get;
            set;
        }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string modifi_full_name
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? modifi_time
        {
            get;
            set;
        }
        #endregion Model
    }
  
    
}
