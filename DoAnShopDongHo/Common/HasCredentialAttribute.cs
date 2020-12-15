using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnShopDongHo
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //{
             //   return false;
           // }
            var session = (Common.UserLogin)HttpContext.Current.Session[Common.CommonConstants.USER_SESSION];
            if(session == null)
            {
                return false;
            }
            List<string> privilegeLevels =  this.GetCredentialByLoggedInUser(session.Username);

            if (privilegeLevels.Contains(this.RoleID) || session.GroupID == CommonConstants.ADIM_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }

        private List<string> GetCredentialByLoggedInUser(string username)
        {
            var credentials = (List < string >)HttpContext.Current.Session[Common.CommonConstants.SESSION_CREFENTIALS];
            return credentials;
        }
    }
}