namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StaffController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult TAList()
        {
            return this.View();
        }

        public ActionResult AddTAToCourse()
        {
            return this.View();
        }
    }
}
