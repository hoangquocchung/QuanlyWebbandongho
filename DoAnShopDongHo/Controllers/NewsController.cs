using DataBaseIO.DBIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            var model = new ContentDao().ListBycontent();
            ViewBag.cate = new CategoryDao().ListAllCate();
            return View(model);
        }

        public ActionResult ContentDetail(long id)
        {
            var model = new ContentDao().GetByID(id);
            return View(model);
        }
    }
}