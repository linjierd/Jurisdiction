using Permission.Bll.Common;
using Permission.Bll.SystemManager;
using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Permission.WebManager.Controllers
{
    [UserPermissionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取登录用户
        /// </summary>
        public static Permission.Model.ViewModel.LoginUserViewModel LoginUser
        {
            get { return AdminUserBll.GetLoginUser(); }
        }
        /// <summary>
        /// 获取请求url
        /// </summary>
        /// <returns></returns>
        public string AppUrl()
        {
            string controller = RouteData.Values["controller"].ToString();
            string action = RouteData.Values["action"].ToString();
            return (controller + "/" + action).ToUpper();
        }
        /// <summary>
        /// 返回当前请求的Module和它的上一级Module 下标0是上级Module，下标1是当前UrlModule
        /// </summary>
        /// <returns></returns>
        public List<ModuleDb> GetThisModuleAndParentMoudule()
        {
            ModuleBll moduleBll = new ModuleBll();
            string url = AppUrl();
            ModuleDb thisModule = moduleBll.GetModuleOnUrl(url);
            List<ModuleDb> list = new List<ModuleDb>();
            if (thisModule != null)
            {
                ModuleDb parentModule = moduleBll.GetModule(thisModule.parent_code);

                if (thisModule != null && parentModule != null)
                {
                    list.Add(parentModule);
                    list.Add(thisModule);
                }
            }


            return list;

        }
        /// <summary>
        /// 获取面包屑导航
        /// </summary>
        /// <returns></returns>
        public string UrlBreadcrumbNavigation()
        {
            List<ModuleDb> list = GetThisModuleAndParentMoudule();
            if (list.Count == 2)
            {
                string str = list[0].module_name + "<span class=\"c-gray en\">&gt;</span>" + list[1].module_name;
                ViewBag.UrlBreadcrumbNavigation = str;
                return str;
            }
            ViewBag.UrlBreadcrumbNavigation = "";
            return "";
        }

    }
}