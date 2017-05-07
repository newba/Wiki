﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string langue = Thread.CurrentThread.CurrentUICulture.ToString();
            HttpCookie cultureCookie = Request.Cookies["langue"];
            if(cultureCookie != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCookie.Value);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
        }
    }
}