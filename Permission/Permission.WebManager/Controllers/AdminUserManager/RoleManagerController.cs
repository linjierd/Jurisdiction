using Newtonsoft.Json.Linq;
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
    public class RoleManagerController : BaseController
    {
        // GET: RoleManager
        public ActionResult List()
        {
            UrlBreadcrumbNavigation();
            return View();
        }

        public ActionResult GetList(SearchModel m)
        {
            var result =  RoleBll.Instance.GetRoleDbPageList(m).ToDataTablesObject().
               Append(t => t.Add("status", c => ((CommonStatus)c.role_status).GetDescription())
               .Add("edit", c => "<a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdate(" + c.role_id + ")\" >编辑</a>")).GetDataTableJson(m);
            return Json(result);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id = 0)
        {
            RoleDb dbModel = new RoleDb();
            RoleWebM model = new RoleWebM();
            if (id > 0)
            {
                ViewBag.EditType = 2;
                dbModel = RoleBll.Instance.GetModel(id);
                ModelCopier.CopyModel(dbModel, model);
            }
            else
            {
                model.role_status = 1;
                ViewBag.EditType = 1;
            }
            ViewBag.Tree =ModuleBll.Instance.GetModuleMacheRoleTree(id);
            return View(model);
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(RoleWebM model)
        {
            RoleBll roleBll = new RoleBll();
            if (ModelState.IsValid)
            {
                RoleDb dbModel = new RoleDb();
                ModelCopier.CopyModel(model, dbModel);
                List<RoleModuleRelationDb> roleModuleRelations = new List<RoleModuleRelationDb>();
                if (!string.IsNullOrEmpty(model.power))
                {
                    string[] powerList = model.power.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in powerList)
                    {
                        RoleModuleRelationDb relation = new RoleModuleRelationDb();
                        relation.module_code = s;
                        relation.creator_date = DateTime.Now;
                        relation.creator_name = LoginUser.user_name;
                        relation.role_id = model.role_id;
                        roleModuleRelations.Add(relation);
                    }
                }
                if (model.role_id > 0)
                {
                        dbModel.modifi_name = LoginUser.user_name;
                        dbModel.modifi_date = DateTime.Now;
                        RoleBll.Instance.Update(dbModel, roleModuleRelations);
                        return Content("<script>alert('修改角色成功!');parent.layer.closeAll('iframe');</script>");
                    
                }
                else
                {
                  
                    dbModel.creator_name = LoginUser.user_name;
                    dbModel.creator_date = DateTime.Now;
                    RoleBll.Instance.Add(dbModel, roleModuleRelations);
                    return Content("<script>alert('添加角色成功!');parent.layer.closeAll('iframe');</script>");
                }
            }
            ViewBag.Tree =  ModuleBll.Instance.GetModuleMacheRoleTree(model.role_id);
            return View(model);

        }

        public ActionResult GetRoleJson(string keyWord, int pageSize, int pageIndex = 1)
        {
            List<RoleDb> roleList = RoleBll.Instance.GetRoleListAllInCache();
            if (!string.IsNullOrEmpty(keyWord))
            {

                roleList = roleList.Where(c => c.role_name.Contains(keyWord)).ToList();
            }
            roleList = (from r in roleList select r).Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
            JObject rss = new JObject(

                    new JProperty("total", roleList.Count),
                    new JProperty("results",
                                  new JArray(
                                      from r in roleList
                                      select new JObject(
                                          new JProperty("id", r.role_id)
                                          , new JProperty("text", r.role_name)
                                          )
                                      )
                                                 )
                );
            return Content(rss.ToString());
        }
    }
}