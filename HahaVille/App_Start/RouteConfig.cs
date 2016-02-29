using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HahaVille
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Play",
            url: "games/play/{name}",
            defaults: new { controller = "Game", action = "Play", name = UrlParameter.Optional });

            routes.MapRoute(
               name: "Category",
               url: "games/category/{name}",
               defaults: new { controller = "Game", action = "Category", name = UrlParameter.Optional });

            routes.MapRoute(
             name: "Game",
             url: "games/{name}",
             defaults: new { controller = "Game", action = "Details", name = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
