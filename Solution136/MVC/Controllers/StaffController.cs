namespace MVC.Controllers
{
    using MVC.App_Start;
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


        [CustomAuthorizeAttribute(RoleList = new string[] { "admin" })]
        public ActionResult CreateTA()
        {
            return this.View();
        }
    }
}
