using Permission.Bll.SystemManager;
using Permission.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Bll.Common
{
    public class UserPermissionFilter:System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            #region 判断是否有 此 controller 和 action 的权限
            LoginUserViewModel user = AdminUserBll.GetLoginUser();
            var permissionListAll = ModuleBll.Instance.GetLevelModuleListIsArrayAllInCache();
            if (null != user && !string.IsNullOrEmpty(user.user_name))
            {
                bool ret = true;
                if (user.PermissionList != null && user.PermissionList.Count > 0)
                {
                    string action = filterContext.ActionDescriptor.ActionName;
                    string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    string url = (controller + "/" + action).ToUpper();
                    var module = permissionListAll.FirstOrDefault(c => c.action_url.ToUpper() == url);
                    if ((controller.ToUpper() != "HOME" && action.ToUpper() != "LOGIN") &&
                        controller.ToUpper() != "MENU" && module != null) /*此Action下的不作权限*/
                                                         //&& module != null
                    {
                        ret = AdminUserBll.LoginUserIsPermission(controller, action, user);
                    }
                    if (!ret)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/NoRight", true);
                        throw new System.Web.HttpException(403, "无权访问");
                    }
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/login", true);

            }


            #endregion

            base.OnActionExecuting(filterContext);
        }
    }
}
