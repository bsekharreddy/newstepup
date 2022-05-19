using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StepUpPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
           name: "FetchByPSNum",
           url: "Login/FetchByPSNum/{traineePSNum}",
           defaults: new { controller = "Login", action = "FetchByPSNum", traineePSNum = UrlParameter.Optional }
           );
            routes.MapRoute(
          name: "FetchByPSNum1",
          url: "Login/FetchByPSNum1/{traineePSNum}",
          defaults: new { controller = "Login", action = "FetchByPSNum1", traineePSNum = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Session", action = "LoginDet", id = UrlParameter.Optional }
            );
        }
    }
}
