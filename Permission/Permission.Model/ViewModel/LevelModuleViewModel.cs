using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Model.ViewModel
{
    public class LevelModuleViewModel
    {
        /// <summary>
        /// 功能code
        /// </summary>
        public string module_code
        {
            get;
            set;
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string module_name
        {
            get;
            set;
        }
        /// <summary>
        /// 父cdoe
        /// </summary>
        public string parent_code
        {
            get;
            set;
        }
        /// <summary>
        /// 功能类别
        /// </summary>
        public int module_level
        {
            get;
            set;
        }
        /// <summary>
        /// 是否菜单
        /// </summary>
        public int? is_menu
        {
            get;
            set;
        }
        /// <summary>
        /// 是否请求
        /// </summary>
        public int is_action { get; set; }
        /// <summary>
        /// 请求url
        /// </summary>
        public string action_url { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public decimal order_by { get; set; }

        public int module_status { get; set; }
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
        public DateTime? creator_time
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
        public DateTime? modifi_time
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是父模块
        /// </summary>
         public bool isParent { get; set; }


        /// <summary>
        /// 子列表
        /// </summary>
        public List<LevelModuleViewModel> SonList { get; set; }

    }
}
