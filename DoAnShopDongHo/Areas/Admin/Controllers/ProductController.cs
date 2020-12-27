using DataBaseIO.DBIO;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            int totalRecord = 0;
            var model = new ProductDao().ListAllProduct(searchString, ref totalRecord, page, pageSize);

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
        [ValidateInput(false)]
        public ActionResult Create(Product entity, ProductDetail ID)
        {
            if (ModelState.IsValid)
            {
                var model = new ProductDao().create(entity);
                ID.ID = model;
                var productdetail = new ProductDetailsDao().Create(ID);
                if (model > 0 && productdetail > 0)
                {
                    SetAlert("Bạn đã thêm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            SetViewBag();
            return View("Create");
        }

        public JsonResult LoadImage(long id)
        {
            ProductDao dao = new ProductDao();
            var product = dao.ViewDetail(id);
            var images = product.MoreImages;
            XElement xImages = XElement.Parse(images);
            List<string> ListImagesReturn = new List<string>();

            foreach(XElement element in xImages.Elements())
            {
                ListImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = ListImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer serialiez = new JavaScriptSerializer();
            var listImages = serialiez.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var  subStringItem = item.Substring(21);
                xElement.Add(new XElement("Image", subStringItem));
            }
            ProductDao dao = new ProductDao();
            try
            {
                dao.UpdateImage(id, xElement.ToString());

                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }


            
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = new ProductDao().ViewDetail(id);
            SetViewBag(model.ID);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product entity)
        {
            var product = new ProductDao();
            var res = product.Edit(entity);
            if (res)
            {
                SetAlert("Cập nhật dữ liệu thành công", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "cập nhật không thành công");
            }
            SetViewBag(entity.ID);
            return View("Edit");
        }

        public ActionResult Detail(long id)
        {
            var model = new ProductDetailsDao().ViewProductDetail(id);
            ViewBag.Product = new ProductDao().ViewDetail(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(ProductDetail entity,string chedobaohanh,string sizeday,string kieudang,string duongmatkinh)
        {
            var model = new ProductDetailsDao();
            entity.CheDoBaoHanh = chedobaohanh;
            entity.SizeDay = sizeday;
            entity.KhieuDang = kieudang;
            entity.DuongKinhMat = duongmatkinh;
            var res = model.Update(entity);
            if (res)
            {
                return RedirectToAction("Detail", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật không thành công");
            }
            return View("Detail");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductDao().Delete(id);
            new ProductDetailsDao().Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult ChangeStatus(long id)
        {
            var res = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = res
            });
        }


        public void SetViewBag(long? selectId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.ProductCategoryID = new SelectList(dao.ListByCate(), "ID", "NameCategory", selectId);
            //ViewBag.Submenu = new SelectList(dao.ListAllMenu(), "IDMenu", "Text", selectId);
        }


    }
}