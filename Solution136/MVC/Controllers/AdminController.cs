namespace MVC.Controllers
{
    using MVC.App_Start;
    using System.Web.Mvc;

    [CustomAuthorizeAttribute(RoleList = new string[] { "admin" })]
    public class AdminController : Controller
    {
        public ActionResult AddCoursePrereq()
        {
            return this.View();
        }

        public ActionResult CreateCourse()
        {
            return this.View();
        }

        public ActionResult CreateInstructor()
        {
            return this.View();
        }

        public ActionResult EditCourse()
        {
            return this.View();
        }

        public ActionResult EditInstructor()
        {
            return this.View();
        }
    }
}
