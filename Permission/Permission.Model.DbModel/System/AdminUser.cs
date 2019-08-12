using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Permission.Model.DbModel.System
{
    /// <summary>
    /// 实体类 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("admin_user")]
    public class AdminUser
    {
        public AdminUser()
        { }
        #region Model

         [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int user_id{get;set;}
        /// <summary>
        /// 用户名
        /// </summary>
        [Key]
        [Required]
        [StringLength(50)]
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
        
        [StringLength(100)]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string user_full_name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int user_status
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? last_lgoin_time
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string last_login_ip
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string creator_name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string creator_full_name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime creator_date
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string modifi_name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string modifi_full_name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? modifi_date
        {
            get;
            set;
        }
     
        #endregion Model

    }
}
