using IRepository;
using POCO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
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
                errors.Add("Review cannot be null");
                throw new ArgumentException();
            }

            var courseReview = GetReviewsForCourse(review.);

            this.repository.InsertStudent(student, ref errors);
        }

        public List<course_review> GetReviewsForCourse(int course_id, ref List<string> errors)
        {
            if (student == null)
            {
                errors.Add("Student cannot be null");
                throw new ArgumentException();
            }

            if (student.StudentId.Length < 5)
            {
                errors.Add("Invalid student ID");
                throw new ArgumentException();
            }

            this.repository.InsertStudent(student, ref errors);
        }
    }
}
