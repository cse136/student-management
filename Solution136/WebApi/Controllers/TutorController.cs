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

    public class TutorController : ApiController
    {
        [HttpGet]
        public List<schedule_tutor> GetStudentTutorings(string student_id)
        {
            var errors = new List<string>();
            var repository = new TutorRepository();
            var repositoryStudent = new StudentRepository();
            var repositorySchedule = new ScheduleRepository();
            var service = new TutorService(repository, repositoryStudent, repositorySchedule);
            return service.GetStudentTutorings(student_id, ref errors);
        }

        [HttpPost]
        public string InsertTutor(schedule_tutor tutor)
        {
            var errors = new List<string>();
            var repository = new TutorRepository();
            var repositoryStudent = new StudentRepository();
            var repositorySchedule = new ScheduleRepository();
            var service = new TutorService(repository, repositoryStudent, repositorySchedule);
            service.InsertTutor(tutor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateTutor(schedule_tutor tutor)
        {
            var errors = new List<string>();
            var repository = new TutorRepository();
            var repositoryStudent = new StudentRepository();
            var repositorySchedule = new ScheduleRepository();
            var service = new TutorService(repository, repositoryStudent, repositorySchedule);
            service.UpdateTutor(tutor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteTutor(string id, int schedule_id)
        {
            var errors = new List<string>();
            var repository = new TutorRepository();
            var repositoryStudent = new StudentRepository();
            var repositorySchedule = new ScheduleRepository();
            var service = new TutorService(repository, repositoryStudent, repositorySchedule);
            service.DeleteTutor(id, schedule_id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}
