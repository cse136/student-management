namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class AdminController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult InstructorList()
        {
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult CreateInstructor()
        {
            return this.View();
        }

        public ActionResult CreateTA()
        {
            return this.View();
        }

        public ActionResult EditTA()
        {
            return this.View();
        }

        public ActionResult CreateScheduledCourse()
        {
            return this.View();
        }

        public ActionResult AddPrereq()
        {
            return this.View();
        }

        public ActionResult EditScheduledCourse()
        {
            return this.View();
        }

        public ActionResult EditInstructor()
        {
            return this.View();
        }

        public ActionResult EditStudent(string id)
        {
            return this.View();
        }

        public ActionResult TAList()
        {
            return this.View();
        }

        public ActionResult ViewCoursePrereqs()
        {
            return this.View();
        }
    }
}
