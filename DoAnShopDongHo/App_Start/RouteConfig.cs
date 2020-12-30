using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnDoAnShopDongHo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "ListProductCategoey", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            routes.MapRoute(
                name: "Product Category ID",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "ListProductCategoeyID", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            routes.MapRoute(
                name: " CategoryID",
                url: "san-pham",
                defaults: new { controller = "Product", action = "ListProductCate", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );
            routes.MapRoute(
                name: "News Detail",
                url: "tin-tuc/{metatitle}-{id}",
                defaults: new { controller = "News", action = "ContentDetail", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metaTitle}-{id}",
                defaults: new { controller = "Product", action = "Detatil", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );
            routes.MapRoute(
                name: "Payment Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            //Tìm kiếm
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "DoAnShopDongHo.Controllers" }
            );
        }
    }
}
