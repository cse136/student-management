using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class TAController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Index");
        }

        public ActionResult ViewStudentsInEnrollment()
        {
            return View();
        }
    }
}