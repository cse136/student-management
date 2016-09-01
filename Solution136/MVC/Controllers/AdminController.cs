namespace MVC.Controllers
{
    using MVC.App_Start;
    using System.Web.Mvc;

    [CustomAuthorizeAttribute(RoleList = new string[] { "admin" })]
    public class AdminController : Controller
    {
        public ActionResult AddCoursePrereq(int id)
        {
            ViewBag.id = id;
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

        public ActionResult EditCourse(int id)
        {
            ViewBag.id = id;

            return this.View();
        }

        public ActionResult EditInstructor(int id)
        {
            ViewBag.id = id;
            return this.View();
        }
    }
}
