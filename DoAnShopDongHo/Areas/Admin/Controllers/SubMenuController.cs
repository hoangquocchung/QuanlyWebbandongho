using DataBaseIO.DBIO;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class SubMenuController : BaseController
    {
        // GET: Admin/SubMenu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new ProductCategoryDao().ListSubMenu(searchString, ref totalRecord, page, pageSize);
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
        public ActionResult Create(ProductCategory entity)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var res = dao.Create(entity);
                if (res > 0)
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

        public ActionResult Edit(long id)
        {
            var model = new ProductCategoryDao().ViewDetail(id);
            SetViewBag(model.ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory entity)
        {
            var dao = new ProductCategoryDao();
            var res = dao.Edit(entity);
            if (res)
            {
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index", "SubMenu");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật không thành công");
            }
            SetViewBag(entity.ID);
            return View("Edit");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var res = new ProductCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = res
            });
        }

        /// <summary>
        /// Gọi ra danh sách tên theo id bảng ProductCategory
        /// </summary>
        /// <param name="selectId"></param>
        public void SetViewBag(long? selectId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.Submenu = new SelectList(dao.ListAllMenu(), "IDMenu", "Text", selectId);
        }
    }
}