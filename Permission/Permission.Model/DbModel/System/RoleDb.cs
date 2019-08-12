using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Permission.Model.DbModel.System
{
    /// <summary>
    /// 实体类 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("role_db")]
    public class RoleDb
    {
        public RoleDb()
        { }
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int role_id
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string role_name
        {
            get;
            set;
        }
        /// <summary>
        /// 角色状态 1 有效 2无效
        /// </summary>
        public int role_status
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
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

      
    }
}
