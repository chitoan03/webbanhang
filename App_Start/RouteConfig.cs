﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Webbanhang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new [] {"Webbanhang.Controllers"}
            );
            routes.MapRoute(
               name: "Them Hang",
               url: "them-gio-hang",
               defaults: new { controller = "Giohang", action = "ThemGiohang", id = UrlParameter.Optional },
                namespaces: new[] { "Webbanhang.Controllers" }
           );
        }
    }
}
