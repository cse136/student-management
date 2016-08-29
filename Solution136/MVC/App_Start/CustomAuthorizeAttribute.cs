using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MVC.App_Start
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] RoleList { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorized = false;

            if (httpContext != null && RoleList != null)
            {
                object role = httpContext.Session["role"];
                authorized =  RoleList.Contains(role.ToString());
            }

            return authorized;
        }

    }
}