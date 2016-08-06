namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class FinancialAidController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Grants(string type)
        {
            return this.View();
        }

        public ActionResult Loans(string id)
        {
            //// because route definition has {controller}/{action}/{id}, we can access the id value
            //// for the request "/FinancialAid/Loans/government" where the id value will be "government"
            return this.View();
        }
    }
}