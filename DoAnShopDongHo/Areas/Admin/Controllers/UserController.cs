using DataBaseIO.DBIO;
using DoAnShopDongHo.Common;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.ChuoiTimKiem = searchString;
            return View(model);
        }
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var MhMd5 = MaHoaMd5.MD5Hash(user.Password);
                user.Password = MhMd5;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }

            }
            return View("Insert");
        }

        public ActionResult Update(long id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var MhHd5 = MaHoaMd5.MD5Hash(user.Password);
                    user.Password = MhHd5;
                }
                var res = dao.Update(user);
                if (res)
                {
                    SetAlert("Sửa usert thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "cập nhật không thành công");
                }

            }
            return View("Update");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult ChangeStatus(long id)
        {
            var res = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = res
            });
        }
    }
}