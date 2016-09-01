namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface ICourseRepository
    {
        List<course> GetCourseList(ref List<string> errors, string course = null, string level = null);
    }
}
