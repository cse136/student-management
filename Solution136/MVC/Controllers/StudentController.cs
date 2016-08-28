namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        // Probably do it with one click from the schedule page :)
        public ActionResult EnrollToCourse()
        {
            return this.View();
        }

        public ActionResult EnrollAsTutor()
        {
            return this.View();
        }
        public ActionResult DropTutoredCourse()
        {
            return this.View();
        }

        // Probably do it with one click from the schedule page :)
        public ActionResult DropCourse()
        {
            return this.View();
        }

        public ActionResult CreateReview()
        {
            return this.View();
        }
    }
}
