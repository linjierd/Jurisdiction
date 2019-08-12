using Permission.Bll.SystemManager;
using Permission.Library;
using Permission.Library.Common;
using Permission.Model.Common.GlobalCode;
using Permission.Model.DbModel.System;
using Permission.WebManager.Lib.WebModel.SystemManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Permission.WebManager.Controllers.AdminUserManager
{
    public class DictionaryTypeManagerController : BaseController
    {
        // GET: DictionaryTypeManager
        public ActionResult List()
        {
            UrlBreadcrumbNavigation();
            return View();
        }

        public ActionResult GetList(SearchModel m)
        {
            var result = DictionaryTypeTableBll.Instance.GetPageList(m).ToDataTablesObject().
               Append(t => t.Add("edit", c => "<a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdate('" + c.dt_type_key + "')\" >编辑</a>")).GetDataTableJson(m);
            return Json(result);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string key = "")
        {
            DictionaryTypeWebM webModel = new DictionaryTypeWebM();
            if (string.IsNullOrEmpty(key))
            {
                ViewBag.EditType = false;
            }
            else
            {
                ViewBag.EditType = true;
                DictionaryTypeTableDb model = DictionaryTypeTableBll.Instance.GetModel(key);
                ModelCopier.CopyModel(model, webModel);
            }
            return View(webModel);
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(DictionaryTypeWebM model,bool isEdit)
        {
            if (ModelState.IsValid)
            {
                model.dt_type_key = model.dt_type_key.Trim();
                DictionaryTypeTableDb dbModel = new DictionaryTypeTableDb();
                ModelCopier.CopyModel(model, dbModel);
                if (isEdit)
                {
                    dbModel.modifi_name = LoginUser.user_name;
                    dbModel.modifi_date = DateTime.Now;
                    DictionaryTypeTableBll.Instance.Update(dbModel);
                    return Content("<script>alert('修改字典类别成功!');parent.layer.closeAll('iframe');</script>");
                }
                else
                {
                    dbModel.creator_name = LoginUser.user_name;
                    dbModel.creator_date = DateTime.Now;
                    DictionaryTypeTableBll.Instance.Add(dbModel);
                    return Content("<script>alert('添加字典类别成功!');parent.layer.closeAll('iframe');</script>");
                }
            }
            ViewBag.EditType = isEdit;
            return View(model);

        }
        /// <summary>
        ///  验证用户名是否可以使用
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult ValidateTypeKey(string dt_type_key)
        {
            if (DictionaryTypeTableBll.Instance.GetModel(dt_type_key) == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("字典类别key已经存在", JsonRequestBehavior.AllowGet);
        }
    }
}