using DataBaseIO.DBIO;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            int totalRecord = 0;
            var model = new ContentDao().ListAllContent(searchString, ref totalRecord, page, pageSize);

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
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                long id = dao.Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao().GetByID(id);
            SetViewBag(dao.CategoryID);
            return View(dao);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            var order = new ContentDao();
            var res = order.Edit(model);
            if (res)
            {
                //SetAlert("Cập nhật dữ liệu thành công", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "cập nhật không thành công");
            }
            SetViewBag(model.CategoryID);
            return View("Edit");
        }
        public string UploadImage(HttpPostedFileBase file)
        {
            //vaildate 
            //xử lí upload
            file.SaveAs(Server.MapPath("~/Data/images/asd/" + file.FileName));// chỉ ra đường dẫn tương đối của nó
            return "/Data/images/asd/" + file.FileName;
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
    }
}