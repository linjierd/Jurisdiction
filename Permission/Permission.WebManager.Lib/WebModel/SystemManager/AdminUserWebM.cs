using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Permission.WebManager.Lib.WebModel.SystemManager
{
    public class AdminUserWebM
    {

        [Required]
        [StringLength(50)]
        [Display(Name = "用户名", Order = 2)]
        [Remote("ValidateUserName", "AdminUserManager")]
        public  string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// 用户密码
        /// </summary> 
        [Required]
        [Display(Name = "密码", Order = 3)]
        [StringLength(100)]
        public  string pass_word
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "验证密码", Order = 4)]
        [System.ComponentModel.DataAnnotations.Compare("pass_word")]
        public  string ConfirmPassword { get; set; }

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
        /// 角色id列表,以逗号分隔
        /// </summary>
        [Required]
        public string role_ids
        {
            get; set;
        }
       public string role_names
        {
            get;set;
        }
    }
    /// <summary>
    /// 更新model
    /// </summary>
    public class AdminUserWebMUpdate : AdminUserWebM
    {
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
    }


    public class UserPassWordUpdate
    {

        [Required]
        public string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// 用户密码
        /// </summary> 
        [Required]
        [Display(Name = "用户密码", Order = 4)]
        [StringLength(100)]
        public string pass_word
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "验证密码", Order = 4)]
        [System.ComponentModel.DataAnnotations.Compare("pass_word")]
        public string ConfirmPassword { get; set; }
    }
}