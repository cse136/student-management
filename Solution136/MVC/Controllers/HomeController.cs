namespace MVC.Controllers
{
    using MVC.App_Start;
    using System;
    using System.Web.Mvc;

    [CustomAuthorizeAttribute(RoleList = new string[] { "admin", "staff", "student" })]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
