namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;

    public class ReviewRepository : IReviewRepository
    {
        public void InsertReview(course_review review, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                context.course_review.Add(review);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }
        }

        public ICollection<course_review> GetCourseReviews(int course_id, ref List<string> errors)
        {
            var list = new List<course_review>();
            var context = new cse136Entities();

            try
            {
                list = context.course_review.Include("course_schedule").Where(r => r.course_schedule.course_id == course_id).ToList();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }

            return list;
        }

        public bool StudentTookCourse(string student_id, int course_schedule_id, ref List<string> errors)
        {
            var list = new List<enrollment>();
            var context = new cse136Entities();

            bool tookCourse = false;

            try
            {
                int count = context.enrollments.Count(e => e.schedule_id == course_schedule_id && e.student_id == student_id);
                tookCourse = count > 0;
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e.Message);
            }
            finally
            {
                context.Dispose();
            }

            return tookCourse;
        }

        public ICollection<course_review> GetStudentReviews(string student_id, ref List<string> errors)
        {
            var list = new List<course_review>();
            var context = new cse136Entities();

            try
            {
                list = context.course_review.Where(e => e.student_id == student_id).ToList();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e.Message);
            }
            finally
            {
                context.Dispose();
            }

            return list;
        }
    }
}
