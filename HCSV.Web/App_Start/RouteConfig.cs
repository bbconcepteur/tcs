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
            #region VIE
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
            #endregion


            #region EN
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
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
