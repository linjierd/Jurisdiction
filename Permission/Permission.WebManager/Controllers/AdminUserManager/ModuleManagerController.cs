using Permission.Bll.SystemManager;
using Permission.Model.DbModel.System;
using Permission.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Permission.WebManager.Controllers
{
    public class ModuleManagerController : BaseController
    {
        /// <summary>
        /// 获取登录用户在指定模块code的子模块
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetLoginUserSonModule(string code)
        {
            List<LevelModuleViewModel> sonList = ModuleBll.Instance.GetSonLevelModuleList(code, LoginUser.PermissionListLevel);
            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Content(javaScriptSerializer.Serialize(sonList));

        }
      

        public ActionResult GetSonModule(string parentCode)
        {
            return Content(ModuleBll.Instance.GetModuleTreeJson(parentCode, 1, false));
        }




        public ActionResult List()
        {
            ViewBag.Tree = ModuleBll.Instance.GetModuleTreeJson("", 2, false);
            UrlBreadcrumbNavigation();
            return View();
        }
        public ActionResult Detail(string paterCode = "", string code = "", bool isReadonly = false)
        {
            ViewBag.IsReadonly = isReadonly;
            ViewBag.PaterName = "";
            ModuleDb model = new ModuleDb();
            if (!string.IsNullOrEmpty(code))
            {
                ViewBag.EditType = 2;
                model = ModuleBll.Instance.GetModule(code);
                if (model != null && !string.IsNullOrEmpty(model.parent_code))
                {
                    paterCode = model.parent_code;
                }
            }
            else
            {
                ViewBag.EditType = 1;
                model.module_code = code;
                model.is_menu = 1;
                model.module_status = 1;

            }
            var pater = ModuleBll.Instance.GetModule(paterCode);
            if (pater != null)
            {
                ViewBag.PaterName = pater.module_name;
                model.parent_code = paterCode;
                model.module_level = pater.module_level + 1;
            }
            else
            {
                model.parent_code = "";
                model.module_level = 1;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Detail(ModuleDb model, bool isGoToList = true, bool isEdit = false)
        {
            if (ModelState.IsValid)
            {
                model.module_code = model.module_code.Trim();
                if (isEdit)
                {
                    if (model != null)
                    {
                        model.modifi_name = LoginUser.user_name;
                        model.modifi_date = DateTime.Now;
                        ModuleBll.Instance.Update(model);
                    }

                }
                else
                {
                    if (ModuleBll.Instance.GetModule(model.module_code) != null)
                    {
                        ModelState.AddModelError("", "该code已经存在!");
                        return View(model);
                    }
                    var pater = ModuleBll.Instance.GetModule(model.parent_code);
                    if (pater != null)
                    {
                        ViewBag.PaterName = pater.module_name;
                        model.parent_code = pater.module_code;
                        model.module_level = pater.module_level + 1;
                    }
                    else
                    {
                        model.parent_code = "";
                        model.module_level = 1;
                    }
                    model.action_url = model.action_url ?? "";
                    model.creator_name = LoginUser.user_name;
                    model.creator_date = DateTime.Now;
                    ModuleBll.Instance.Add(model);
                }
                if (isGoToList) return Content("<script> window.parent.location.reload();</script>");
            }
            return View(model);

        }
    }
}