using Permission.Bll.TestBll;
using Permission.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Permission.Library;
using Permission.Library.Common;

namespace Permission.WebManager.Controllers.TestDemo
{
    public class TestDemoController : BaseController
    {
        // GET: TestDemo
        public ActionResult List()
        {
            UrlBreadcrumbNavigation();
            return View();
        }

        public ActionResult GetList(SearchModel m)
        {
            var result= TestBll.Instance.GetPageList(m).ToDataTablesObject().
                 Append(t =>t.Add("edit", c => "<a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdate('" + c.id + "')\" >编辑</a> <a class=\"ml-5 bg_xiugai\" href=\"JavaScript:void(null)\" onclick=\"openUpdatePwd('" + c.id + "')\" >修改密码</a>")).GetDataTableJson(m);
            return Json(result);
        }
    }
}