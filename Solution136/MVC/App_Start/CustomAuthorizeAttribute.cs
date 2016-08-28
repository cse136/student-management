using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MVC.App_Start
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] RoleList { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            return RoleList.Length > 0 &&
                RoleList.Contains(httpContext.Session["role"].ToString());
           
        }

    }
}