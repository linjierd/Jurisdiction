using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Permission.WebManager.Lib.WebModel.SystemManager
{
    public class RoleWebM
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
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
        [Display(Name = "角色名", Order = 2)]
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

        public string power
        {
            get;
            set;
        }
       

        #endregion Model
    }
}