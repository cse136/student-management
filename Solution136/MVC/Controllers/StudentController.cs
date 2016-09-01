namespace MVC.Controllers
{
    using MVC.App_Start;
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        [CustomAuthorizeAttribute(RoleList = new string[] { "student" })]
        public ActionResult AddTutorToScheduledCourse()
        {
            return this.View();
        }

        [CustomAuthorizeAttribute(RoleList = new string[] { "student" })]
        public ActionResult CreateReview()
        {
            return this.View();
        }

        [CustomAuthorizeAttribute(RoleList = new string[] { "student", "admin", "staff" })]
        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        [CustomAuthorizeAttribute(RoleList = new string[] { "student", "admin", "staff" })]
        public ActionResult Index()
        {
            return this.View();
        }   
    }
}
