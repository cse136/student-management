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

        public ActionResult AddCoursePrereq()
        {
            return this.View();
        }

        public ActionResult AddTAToScheduledCourse()
        {
            return this.View();
        }

        public ActionResult CourseDetails()
        {
            return this.View();
        }

        public ActionResult CourseList()
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

        public ActionResult CreateScheduledCourse()
        {
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult CreateTA()
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

        public ActionResult EditScheduledCourse(string id)
        {
            return this.View();
        }

        public ActionResult EditStudent(string id)
        {
            return this.View();
        }

        public ActionResult EditTA(string id)
        {
            return this.View();
        }


        public ActionResult InstructorList()
        {
            return this.View();
        }

        public ActionResult ScheduledCourseList()
        {
            return this.View();
        }

        public ActionResult ScheduledCourseReviews()
        {
            return this.View();
        }

        public ActionResult ScheduledCourseStudentList()
        {
            return this.View();
        }

        public ActionResult StudentDetails()
        {
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult TADetails()
        {
            return this.View();
        }

        public ActionResult TAList()
        {
            return this.View();
        }
    }
}
