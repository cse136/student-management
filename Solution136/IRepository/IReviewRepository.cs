using POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IReviewRepository
    {
        void InsertReview(course_review review, ref List<string> errors);

        ICollection<course_review> GetCourseReviews(int course_id, ref List<string> errors);
    }
}
