using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DusColl
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcHandler.DisableMvcResponseHeader = true;
        }

        public void Application_Error(object sender, EventArgs e)
        {
            if (Session["Account"] == null)
            {
                new RedirectToRouteResult(new RouteValueDictionary { { "action", "AccountTimeOut" }, { "controller", "Account" } });
            }
            else
            {
                new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Error" } });
            }
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        protected void Application_Beginrequest(object sender, EventArgs e)
        {

            var app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
            }


            try
            {
                if (Session["Account"] == null)
                {
                    new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "account" } });
                }
            }
            catch
            {
                new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "account" } });

            }


        }

    }
}