using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] RoleList { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorized = false;

            if (httpContext != null && RoleList != null)
            {
                object role = httpContext.Session["role"];

                if (role != null)
                {
                    authorized = RoleList.Contains(role.ToString());
                }
            }

            return authorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            object dictionary;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                dictionary = new { controller = "Login", action = "Index" };
            }
            else
            {
                dictionary = new { controller = "Error", action = "AccessDenied" };
            }

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(dictionary));
        }
    }
}