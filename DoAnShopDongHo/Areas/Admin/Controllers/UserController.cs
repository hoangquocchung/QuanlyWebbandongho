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
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.ChuoiTimKiem = searchString;
            return View(model);
        }

        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Insert()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
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
            SetViewBag();
            return View("Insert");
        }

        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Update(long id)
        {
            var user = new UserDao().ViewDetail(id);
            SetViewBag(user.GroupID);
            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
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
            SetViewBag(user.GroupID);
            return View("Update");
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(long id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var res = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = res
            });
        }

        public void SetViewBag(string selectId = null)
        {
            var dao = new UserGroupDao();
            ViewBag.Usergroup = new SelectList(dao.ListAll(), "ID", "Name", selectId);
            //ViewBag.Submenu = new SelectList(dao.ListAllMenu(), "IDMenu", "Text", selectId);
        }
    }
}