namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface ICourseRepository
    {
        List<course> GetCourseList(ref List<string> errors, string course = null, string level = null);

        course GetCourseDetail(int course, ref List<string> errors);

        ICollection<course> GetCoursePrereqs(int course_id, ref List<string> errors);

        void RemovePrereq(int id, int prereq, ref List<string> errors);

        void AddPrereq(int id, int prereq, ref List<string> errors);

        void UpdateCourse(course course, ref List<string> errors);


        void InsertCourse(course course, ref List<string> errors);
    }
}
