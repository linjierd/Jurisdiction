using Newtonsoft.Json.Linq;
using Permission.Bll.SystemManager;
using Permission.Library;
using Permission.Library.Common;
using Permission.Model.Common.GlobalCode;
using Permission.Model.DbModel.System;
using Permission.Model.ViewModel;
using Permission.WebManager.Lib.WebModel.SystemManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Permission.WebManager.Controllers.AdminUserManager
{
                 
    public class AdminUserManagerController :BaseController
    {
        /// <summary>
        /// 列表初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            UrlBreadcrumbNavigation();
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="m">搜索模型</param>
        /// <returns></returns>
        public ActionResult GetList(SearchModel m)
        {
            var result =  AdminUserBll.Instance.GetPageList(m).ToDataTablesObject().
               Append(t => t.Add("status", c => ((CommonStatus)c.user_status).GetDescription())
               .Add("edit", c => "<a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdate('" + c.user_name + "')\" >编辑</a> <a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdatePwd('" + c.user_name + "')\" >修改密码</a>")).GetDataTableJson(m);
            return Json(result);
        }
        /// <summary>
        ///  验证用户名是否可以使用
        /// </summary>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public ActionResult ValidateUserName(string user_name)
        {
            if (AdminUserBll.Instance.GetModel(user_name)==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("用户名已经存在", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Add()
        {
            AdminUserWebM model = new AdminUserWebM();
            model.user_status = (int)CommonStatus.Active;
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(AdminUserWebM model)
        {
            if (ModelState.IsValid && model != null)
            {
                AdminUserDb adminUserDb = new AdminUserDb();
                model.user_name = model.user_name.Trim();
                ModelCopier.CopyModel(model, adminUserDb);
                AdminUserBll.Instance.Add(adminUserDb, model.role_ids);
                return Content("<script>alert('添加用户成功!');parent.layer.closeAll('iframe');</script>");
            }
            return View(model);
        }

        public ActionResult Update(string userName = "")
        {
            if (string.IsNullOrEmpty(userName)) return Redirect("list");
            AdminUserWebMUpdate webModel = new AdminUserWebMUpdate();
            AdminUserDb dbModel = AdminUserBll.Instance.GetModel(userName);
            if (dbModel != null)
            {
                ModelCopier.CopyModel(dbModel, webModel);
                if (dbModel.AdminUserRoleRelations != null && dbModel.AdminUserRoleRelations.Count > 0)
                {
                    webModel.role_ids = string.Join(",", dbModel.AdminUserRoleRelations.Select(c => c.role_id).ToArray());
                    webModel.role_names = string.Join(",", dbModel.AdminUserRoleRelations.Select(c => c.RoleDb.role_name).ToArray());

                }
            }
            return View(webModel);
        }
        [HttpPost]
        public ActionResult Update(AdminUserWebMUpdate model)
        {
            AdminUserDb dbModel = AdminUserBll.Instance.GetModel(model.user_name);
            if (ModelState.IsValid && model != null)
            {
                dbModel.user_status = model.user_status;
                dbModel.user_full_name = model.user_full_name;
                dbModel.modifi_name = LoginUser.user_name;
                dbModel.modifi_date = DateTime.Now;
                AdminUserBll.Instance.Update(dbModel, model.role_ids);
                return Content("<script>alert('修改用户成功!');parent.layer.closeAll('iframe');</script>");
            }
            return View(model);


        }
        public ActionResult UpdatePwd(string userName)
        {
            UserPassWordUpdate model = new UserPassWordUpdate();
            model.user_name = userName;
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdatePwd(UserPassWordUpdate model)
        {
            ViewBag.UserName = model.user_name;
            if (ModelState.IsValid)
            {
                
                AdminUserBll.Instance.UpdateUserPassWord(model.user_name, model.pass_word);
                return Content("<script>alert('密码成功!');parent.layer.closeAll('iframe');</script>");
            }
            return View(model);

        }
     
    }
}