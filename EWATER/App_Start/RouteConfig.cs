using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EWATER
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "AdReport",
              "AdReport/{action}",
              new { controller = "AdReport", action = "ReportView" }
          );

            routes.MapRoute(
               "AdDashboard",
               "AdDashboard/{action}/{date}",
               new { controller = "AdDashboard", action = "DashboardView", date = UrlParameter.Optional }
           );

            routes.MapRoute(
               "ClientOrder",
               "ClientOrder/{action}/{phoneNumber}",
               new { controller = "ClientOrder", action = "ClientOrderView", phoneNumber = UrlParameter.Optional }
           );

            routes.MapRoute(
               "ClientProduct",
               "ClientProduct/{action}",
               new { controller = "ClientProduct", action = "ClientProductView" }
           );

            routes.MapRoute(
               "AdStaff",
               "AdStaff/{action}",
               new { controller = "AdStaff", action = "StaffView" }
           );

            routes.MapRoute(
               "AdProduct",
               "AdProduct/{action}",
               new { controller = "AdProduct", action = "ProductView" }
           );

            routes.MapRoute(
                "AdCustomer",
                "AdCustomer/{action}/{phoneNumber}",
                new { controller = "AdCustomer", action = "CustomerView", phoneNumber = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomeView", id = UrlParameter.Optional }
            );
        }
    }
}
