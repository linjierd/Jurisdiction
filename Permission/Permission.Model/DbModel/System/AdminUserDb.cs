using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Permission.Model.DbModel.System
{
    /// <summary>
    /// 实体类 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("admin_user")]
    public class AdminUserDb
    {
        public AdminUserDb()
        { }
        #region Model


        /// <summary>
        /// 用户名
        /// </summary>
        [Key]
        [Required]
        [StringLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// 用户密码
        /// </summary> 
        [Required]
        [StringLength(100)]
        public string pass_word
        {
            get;
            set;
        }


        /// <summary>
        /// 用户全名
        /// </summary>
        public string user_full_name
        {
            get;
            set;
        }
        /// <summary>
        /// 用户状态 1:有效  2:无效
        /// </summary>
        public int user_status
        {
            get;
            set;
        }
        /// <summary>
        /// 最后登录日期
        /// </summary>
        public DateTime? last_lgoin_date
        {
            get;
            set;
        }
        /// <summary>
        /// 最后登录ip
        /// </summary>
        public string last_login_ip
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人登录名
        /// </summary>
        public string creator_name
        {
            get;
            set;
        }
       
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime creator_date
        {
            get;
            set;
        }
        /// <summary>
        /// 修改人登录名
        /// </summary>
        public string modifi_name
        {
            get;
            set;
        }
        
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? modifi_date
        {
            get;
            set;
        }
        #endregion Model

        public virtual List<AdminUserRoleRelationDb> AdminUserRoleRelations { get; set; }
    }
}
