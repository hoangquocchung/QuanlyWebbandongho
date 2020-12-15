using DataBaseIO.DBIO;
using DoAnShopDongHo.Areas.Admin.Models;
using DoAnShopDongHo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModels model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var res = dao.Login(model.UserName, MaHoaMd5.MD5Hash(model.PassWord),true);
                if (res == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.Username = user.Username;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    var listCredentials = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREFENTIALS, listCredentials);

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "tài khoản không tồn tại");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "tài khoản đã bị khóa");
                }
                else if (res == -2)
                {
                    ModelState.AddModelError("", "tài khoản không đúng");
                }
                else if (res == -2)
                {
                    ModelState.AddModelError("", "tài khoản của bạn không có quyền đăng nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View("Index");
        }

    }
}