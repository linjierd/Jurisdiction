using Permission.Bll.SystemManager;
using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Permission.WebManager.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginName, string passWord)
        {

            ViewBag.LoginName = loginName;
            ViewBag.PassWord = passWord;
            ViewBag.ErrorMessage = "";
            string md5PassWord = Library.Tools.Text.StringMd5.Md5Hash32Salt(passWord);
            AdminUserDb user = AdminUserBll.Instance.GetModel(loginName);
            if (user != null)
            {
                if (user.pass_word.ToUpper() == md5PassWord.ToUpper())
                {
                    user.last_lgoin_date = DateTime.Now;
                    user.last_login_ip = Request.UserHostAddress;
                    AdminUserBll.Instance.IniLogin(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.errorMessage = "用户密码错误";
                }
            }
            else
            {
                ViewBag.errorMessage = "用户密码错误";
            }
            return View();
        }

        public ActionResult SignOut()
        {
            AdminUserBll.SignOut();
            return Redirect("/Login");
        }
    }
}