using POCO;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class InstructorController : ApiController
    {
        [HttpGet]
        public List<instructor> GetList(string instructor = null, string title = null)
        {
            var service = new InstructorService(new InstructorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetList(ref errors, instructor, title);
        }

        [HttpGet]
        public instructor GetDetail(int id)
        {
            var service = new InstructorService(new InstructorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetDetail(id, ref errors);
        }

       
        [HttpPost]
        public string Edit(instructor instructor)
        {
            var service = new InstructorService(new InstructorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            service.Edit(instructor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string Add(instructor instructor)
        {
            var service = new InstructorService(new InstructorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            service.Add(instructor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}
