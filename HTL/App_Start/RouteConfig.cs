using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DusColl
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DefaultExpired",
                url: "Account",
                defaults: new { controller = "Account", action = "AccountTimeOut" }
            );

            routes.MapRoute(
              name: "AccountChange",
              url: "AccountF",
              defaults: new { controller = "Account", action = "AccountChangeForce" }
            );

            routes.MapRoute(
            name: "AccountChucgrp",
            url: "AccountG",
            defaults: new { controller = "Account", action = "AccountChucgrp" }
            );

            routes.MapRoute(
                    name: "AccountChucpas",
                    url: "AccountCP",
                    defaults: new { controller = "Account", action = "AccountChucpas" }
                    );

            routes.MapRoute(
                    name: "AccountRegistra",
                    url: "AccountRG",
                    defaults: new { controller = "Account", action = "AccountRegis" }
                    );

            routes.MapRoute(
                 name: "AccountRTRegistra",
                 url: "AccountRT",
                 defaults: new { controller = "Account", action = "AccountRegisRT" }
                 );

            routes.MapRoute(
                      name: "HomePages",
                      url: "Home",
                      defaults: new { controller = "Home", action = "HomeGate" }
                    );

            routes.MapRoute(
            name: "UnsupportBrowser",
            url: "UnsupportBrowser",
            defaults: new { controller = "Error", action = "UnsupportBrowser" }
        );


            routes.MapRoute(
            name: "ErroPage",
            url: "Error",
            defaults: new { controller = "Error", action = "Index" }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "LogUserIn" }
            );

            routes.MapRoute(
                name: "AccountIDAktivasi",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "AccountAktivasi" }
            );

        }
    }
}