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
    }
}
