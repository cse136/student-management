namespace MVC.Controllers
{
    using MVC.App_Start;
    using System.Web.Mvc;

    public class StaffController : Controller
    {
        public ActionResult AddTAToScheduledCourse()
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

        public ActionResult EditScheduledCourse(string id)
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
