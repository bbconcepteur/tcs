using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HCSV.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Gioi thieu",
                url: "gioi-thieu",
                defaults: new { controller = "Content", action = "Details"},
                namespaces:new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "About",
                url: "about-us",
                defaults: new { controller = "Content", action = "Details"},
                namespaces: new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Tin tuc",
                url: "tin-tuc",
                defaults: new { controller = "Content", action = "Index" },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "News",
                url: "news",
                defaults: new { controller = "Content", action = "Index" },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Tin tuc",
                url: "tin-tuc/{metatitle}-{id}",
                defaults: new { controller = "Content", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "News",
                url: "news/{metatitle}-{id}",
                defaults: new { controller = "Content", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Tuyen dung",
                url: "tuyen-dung/",
                defaults: new { controller = "Careers", action = "Index" },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Careers",
                url: "careers",
                defaults: new { controller = "Careers", action = "Index" },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Chi tiet tuyen dung",
                url: "tuyen-dung/{metatitle}-{id}",
                defaults: new { controller = "Careers", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Careers detail",
                url: "careers/{metatitle}-{id}",
                defaults: new { controller = "Careers", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "HCSV.Web.Controllers" }
            );*/
            routes.MapRoute(
                name: "Tin tuc",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index" }
            );
            routes.MapRoute(
                name: "Tin tuc chi tiet",
                url: "tin-tuc/{menu}/{metatitle}-{contentID}",
                defaults: new { controller = "News", action = "Details", menu = UrlParameter.Optional, metatitle = UrlParameter.Optional, contentID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "News",
                url: "news",
                defaults: new { controller = "News", action = "Index" }
            );
            routes.MapRoute(
                name: "News detail",
                url: "news/{menu}/{metatitle}-{contentID}",
                defaults: new { controller = "News", action = "Details", menu = UrlParameter.Optional, metatitle = UrlParameter.Optional, contentID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
