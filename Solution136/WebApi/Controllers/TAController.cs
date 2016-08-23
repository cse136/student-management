namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class TAController : ApiController
    {
        [HttpGet]
        public List<TeachingAssistant> GetCourseTAs(int schedule_id)
        {
            var errors = new List<string>();
            var repository = new TARepository();
            var service = new TAService(repository);
            return (List<TeachingAssistant>)service.GetCourseTAs(schedule_id, ref errors);
        }

        [HttpPost]
        public string InsertTA(TeachingAssistant ta)
        {
            var errors = new List<string>();
            var repository = new TARepository();
            var service = new TAService(repository);
            service.InsertTA(ta, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateTA(TeachingAssistant ta)
        {
            var errors = new List<string>();
            var repository = new TARepository();
            var service = new TAService(repository);
            service.UpdateTA(ta, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteTA(int id, int schedule_id)
        {
            var errors = new List<string>();
            var repository = new TARepository();
            var service = new TAService(repository);
            service.DeleteTA(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}
