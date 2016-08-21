namespace IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using POCO;

    public interface IReviewRepository
    {
        void InsertReview(course_review review, ref List<string> errors);

        ICollection<course_review> GetCourseReviews(int course_id, ref List<string> errors);

        ICollection<course_review> GetStudentReviews(string student_id, ref List<string> errors);

        bool StudentTookCourse(string student_id, int course_schedule_id, ref List<string> errors);
    }
}
