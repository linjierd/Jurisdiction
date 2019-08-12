using Permission.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Permission.Bll.SystemManager;
using Permission.Model.Common.GlobalCode;
using Permission.Model.DbModel.System;
using Permission.Library.Common;
using Permission.WebManager.Lib.WebModel.SystemManager;

namespace Permission.WebManager.Controllers.AdminUserManager
{
    public class DictionaryManagerController :BaseController
    {
        // GET: DictionaryManager
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
            var result = DictionaryTableBll.Instance.GetPageList(m).ToDataTablesObject().
               Append(t => 
               t.Add("dictionary_type_name", c =>c.DictionaryTypeTableDb.dt_type_name)
               .Add("status", c =>((CommonStatus)c.dt_status).GetDescription())
               .Add("edit", c => "<a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdate('" + c.dt_key + "')\" >编辑</a>")
               ).GetDataTableJson(m);
            return Json(result);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string key = "")
        {
            DictionaryWebM webModel = new DictionaryWebM();
            if (string.IsNullOrEmpty(key))
            {
                ViewBag.EditType = false;
            }
            else
            {
                ViewBag.EditType = true;
                DictionaryTableDb model = DictionaryTableBll.Instance.GetModel(key);
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
        public ActionResult Detail(DictionaryWebM model, bool isEdit)
        {
            if (ModelState.IsValid)
            {
                model.dt_key = model.dt_key.Trim();
                DictionaryTableDb dbModel = new DictionaryTableDb();
                ModelCopier.CopyModel(model, dbModel);
                if (isEdit)
                {
                    dbModel.modifi_name = LoginUser.user_name;
                    dbModel.modifi_date = DateTime.Now;
                    DictionaryTableBll.Instance.Update(dbModel);
                    return Content("<script>alert('修改字典成功!');parent.layer.closeAll('iframe');</script>");
                }
                else
                {
                    dbModel.creator_name = LoginUser.user_name;
                    dbModel.creator_date = DateTime.Now;
                    DictionaryTableBll.Instance.Add(dbModel);
                    return Content("<script>alert('添加字典成功!');parent.layer.closeAll('iframe');</script>");
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
        public ActionResult ValidateKey(string dt_key)
        {
            if (DictionaryTableBll.Instance.GetModel(dt_key) == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("字典key已经存在", JsonRequestBehavior.AllowGet);
        }
    }
}