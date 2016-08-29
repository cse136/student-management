namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        public ActionResult AddTutorToScheduledCourse()
        {
            return this.View();
        }

        public ActionResult CreateReview()
        {
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult Index()
        {
            return this.View();
        }   
    }
}
