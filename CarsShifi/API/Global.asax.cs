using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            // בדיקה אם הבקשה היא מסוג OPTIONS (הבדיקה המקדימה של הדפדפן)
            if (Context.Request.HttpMethod == "OPTIONS")
            {
                // הוספת הכותרות הדרושות ידנית למקרה שה-CORS הרגיל מפספס אותן
                Context.Response.AddHeader("Access-Control-Allow-Origin", Context.Request.Headers["Origin"]);
                Context.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
                Context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                Context.Response.AddHeader("Access-Control-Allow-Credentials", "true");

                // עצירת הבקשה כאן והחזרת סטטוס OK (200)
                Context.Response.StatusCode = 200;
                Context.Response.End();
            }
        }

    }
}
