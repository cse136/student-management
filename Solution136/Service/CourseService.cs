namespace Service
{
    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class CourseService
    {
        private readonly ICourseRepository repository;

        public CourseService(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public List<course> GetCourseList(ref List<string> errors, string course = null, string level = null)
        {
            return this.repository.GetCourseList(ref errors, course, level);
        }

        public course GetCourseDetail(int id, ref List<string> errors)
        {
            var course = this.repository.GetCourseDetail(id, ref errors);
            course.prereqs = (List<course>)this.repository.GetCoursePrereqs(id, ref errors);
            return course;
        }

        public void RemovePrereq(int id, int prereq, ref List<string> errors)
        {
            this.repository.RemovePrereq(id, prereq, ref errors);
        }

        public void AddPrereq(int id, int prereq, ref List<string> errors)
        {
            this.repository.AddPrereq(id, prereq, ref errors);
        }

        public void Edit(course course, ref List<string> errors)
        {
            this.repository.UpdateCourse(course, ref errors);
        }

        public void Add(course course, ref List<string> errors)
        {
            this.repository.InsertCourse(course, ref errors);
        }
    }
}
