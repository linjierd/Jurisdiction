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
    [Table("admin_user_role_relation")]
    public class AdminUserRoleRelationDb
    {
        public AdminUserRoleRelationDb()
        { }
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// 角色id
        /// </summary>
        public int role_id
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
        #endregion Model

       
        public virtual AdminUserDb AdminUser { get; set; }

        public virtual RoleDb RoleDb { get; set; }
    }
}
