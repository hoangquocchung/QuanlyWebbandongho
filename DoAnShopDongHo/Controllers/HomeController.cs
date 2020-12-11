using DataBaseIO.DBIO;
using DoAnShopDongHo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ProductCategory = new ProductCategoryDao().ListByProductCategory(3);
            var product = new ProductDao();
            ViewBag.NewProduct = product.ListByProduct(8);
            ViewBag.FeatureProduct = product.ListFeatureProduct(8);
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = new MenuDao().ListAll(1);
            ViewBag.ProductCategory = new ProductCategoryDao().ListAllProductCatregory();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult HeaderMain()
        {
            var model = new MenuDao().ListAll(1);
            ViewBag.ProductCategory = new ProductCategoryDao().ListAllProductCatregory();

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult NumberCart()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}