using DataBaseIO.DBIO;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new MenuDao().ListAllMenu(searchString, ref totalRecord, page, pageSize);
            ViewBag.ChuoiTimKiem = searchString;

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

        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Menu entity)
        {
            if (ModelState.IsValid)
            {
                var model = new MenuDao().Create(entity);
                if (model > 0)
                {
                    SetAlert("Bạn đã thêm thành công", "success");
                    return RedirectToAction("Index", "SubMenu");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            SetViewBag();
            return View("Create");
        }

        public ActionResult Edit(int id)
        {
            var model = new MenuDao().ViewDetail(id);
            SetViewBag(model.IDMenu);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Menu entity)
        {
            var model = new MenuDao();
            var res = model.Edit(entity);
            if (res)
            {
                SetAlert("Cập nhật dữ liệu thành công", "success");
                return RedirectToAction("Index", "menu");
            }
            else
            {
                ModelState.AddModelError("", "Thêm không thành công");
            }
            SetViewBag(entity.IDMenu);
            return View("Edit");
        }

        public void SetViewBag(int? selectId = null)
        {
            var dao = new MenuDao();
            ViewBag.menuType = new SelectList(dao.ListAllMenutype(), "ID", "Name", selectId);
        }
    }
}