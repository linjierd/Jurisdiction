using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Model.ViewModel
{
    public class LoginUserViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
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
        /// 不带层级的权限
        /// </summary>
        public List<ModuleDb> PermissionList { get; set; }
        /// <summary>
        /// 带层级的权限列表
        /// </summary>
        public List<LevelModuleViewModel> PermissionListLevel { get; set; }
    }
}
