using DoAnShopDongHo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", action = "Index", Areas = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alter-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alter-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alter-danger";
            }
        }
    }
}