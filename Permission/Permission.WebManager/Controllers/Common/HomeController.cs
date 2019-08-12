using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Permission.WebManager.Controllers
{
    public class HomeController :BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NoRight()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}