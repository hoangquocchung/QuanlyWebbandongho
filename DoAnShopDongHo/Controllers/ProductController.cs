using DataBaseIO.DBIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detatil(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.ListRelatedProduct = new ProductDao().ListRelatedProduct(id);
            ViewBag.productDetail = new ProductDetailsDao().ViewProductDetail(id);
            return View(product);
        }

        public ActionResult ListProductCategoey(long id, int page = 1, int pageSize = 12)
        {
            var ProductCategory = new ProductCategoryDao().ViewDetail(id);
            ViewBag.productCategory = ProductCategory;

            long totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(id, ref totalRecord, page, pageSize);

            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = maxPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}