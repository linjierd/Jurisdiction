using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        [Display(Name = "模块code", Order = 15001)]
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
        [Display(Name = "模块名称", Order = 15002)]
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
        [Display(Name = "父模块Code", Order = 15000)]
        public string parent_code
        {
            get;
            set;
        }
        /// <summary>
        /// 功能级别
        /// </summary>
         [Required]
        [Display(Name = "级别", Order = 15003)]
        public int module_level
        {
            get;
            set;
        }
        /// <summary>
        /// 是否菜单
        /// </summary>
         [Required]
        [Display(Name = "是否菜单", Order = 15004)]
        public int is_menu
        {
            get;
            set;
        }
        /// <summary>
        /// 是否请求
        /// </summary>
         [Required]
        [Display(Name = "是否是请求", Order = 15005)]
        public int is_action { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        [Display(Name = "请求url")]
        public string action_url { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Display(Name = "显示顺序")]
        [Required]
         public decimal order_by { get; set; }
        /// <summary>
        /// 模块状态  1:有效  2:无效
        /// </summary>
        [Display(Name = "是否有效")]
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
        /// 创建时间
        /// </summary>
        public DateTime creator_date
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
        /// 修改时间
        /// </summary>
        public DateTime? modifi_date
        {
            get;
            set;
        }
        #endregion Model

        public virtual List<RoleModuleRelationDb> RoleModuleRelations { get; set; }
    }
  
    
}
