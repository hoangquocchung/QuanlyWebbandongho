using DataBaseIO.DBIO;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            int totalRecord = 0;
            var model = new ProductCategoryDao().ListAllProductCate(searchString, ref totalRecord, page, pageSize);
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
        [HttpGet]
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
                var model = new ProductCategoryDao().Create(entity);
                if(model > 0)
                {
                    SetAlert("Bạn đã thêm thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
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
        public ActionResult Edit(ProductCategory model)
        {
            var dao = new ProductCategoryDao();
            var res = dao.Edit(model);
            if (res)
            {
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index", "ProductCategory");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật không thành công");
            }
            SetViewBag(model.ID);
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
            ViewBag.ProductCategoryID = new SelectList(dao.ListAllSubcategory(), "ID", "NameCategory", selectId);
            //ViewBag.Submenu = new SelectList(dao.ListAllMenu(), "IDMenu", "Text", selectId);
        }
    }
}