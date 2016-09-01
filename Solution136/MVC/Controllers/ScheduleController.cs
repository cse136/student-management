namespace MVC.Controllers
{
    using MVC.App_Start;
    using System.Web.Mvc;

    [CustomAuthorizeAttribute(RoleList = new string[] { "student", "admin", "staff" })]
    public class ScheduleController : Controller
    {
        public ActionResult CourseDetails()
        {
            return this.View();
        }

        public ActionResult CourseList()
        {
            return this.View();
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ScheduledCourseReviews()
        {
            return this.View();
        }

        public ActionResult ScheduledCourseDetails()
        {
            return this.View();
        }
    }
}
