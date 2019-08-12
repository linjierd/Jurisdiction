using Permission.Library;
using Permission.Library.EntitySearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Permission.WebManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        ///   注册ModelBinder
        /// </summary>
        private static void RegisterBinders()
        {
            ModelBinders.Binders.Add(typeof(SearchModel), new SearchModelBinder());
        }
        protected void Application_Start()
        {
         
            AreaRegistration.RegisterAllAreas();
            RegisterBinders();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
