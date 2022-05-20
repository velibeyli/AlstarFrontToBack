using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontToBack.Areas.Admin.Controllers
{
    public class AuthorizeAdminFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if (HttpContext.Current.Session["adminLogged"] == null)
                filterContext.Result = new RedirectResult("/Admin/Account/Login");
        }
    }
}