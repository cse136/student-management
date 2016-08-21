namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;
    using Repository;

    public class ReviewService
    {
        private readonly IReviewRepository repository;

        public ReviewService(IReviewRepository repository)
        {
            this.repository = repository;
        }

        public void InsertReview(course_review review, ref List<string> errors)
        {
            if (review == null)
            {
                throw new ArgumentNullException();
            }

            if (review.rating < 1 || review.rating > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (!this.repository.StudentTookCourse(review.student_id, review.schedule_id, ref errors))
            {
                errors.Add("Student can't review a course not taken");
            }

            var reviews = this.repository.GetStudentReviews(review.student_id, ref errors);

            if (reviews.Count(r => r.schedule_id == review.schedule_id) > 0)
            {
                errors.Add("Student reviewed the course already");
            }

            if (errors.Count() == 0)
            {
                this.repository.InsertReview(review, ref errors);
            }
        }

        public List<course_review> GetReviewsForCourse(int course_id, ref List<string> errors)
        {
            return new List<course_review>();
        }
    }
}
