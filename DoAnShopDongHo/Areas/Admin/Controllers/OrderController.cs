using DataBaseIO.DBIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(long? id,string searchString, int page = 1, int pageSize = 4)
        {
            int totalRecord = 0;
            var model = new OrderDao().ListAllOrder(searchString, ref totalRecord, page, pageSize);

            ViewBag.ChuoiTimKiem = searchString;
            long IDparam = 1;
            if (id == null)
            {
                ViewBag.orderDetail = new OrderDetailDao().ListOrdetail(IDparam);
            }
            else
            {
                IDparam = (long)id;
                ViewBag.orderDetail = new OrderDetailDao().ListOrdetail(IDparam);
            }
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

        public JsonResult ChangeStatus(long id)
        {
            var res = new OrderDao().ChangeStatus(id);
            return Json(new
            {
                status = res
            });
        }

    }
}