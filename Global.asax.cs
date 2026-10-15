using System;
using System.Collections.Generic;
using System.Web;
using PEESEE_GAMING.Models;
using System.Web.Routing;
using System.Web.Optimization;
using PEESEE_GAMING.Models;

namespace PEESEE_GAMING
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["Products"] = ProductData.GetProducts();

            Application["Users"] = new List<StoreUser>();

            Application["Orders"] = new List<StoreOrder>();
        }
    }
}