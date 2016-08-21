namespace ServiceTest
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class ReviewServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReviewNull()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
   
            //// Act
            reviewService.InsertReview(null, ref errors);
        }

        [TestMethod]
        public void StudentCantReviewMoreThanOnce()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 5 };
            var reviews = new List<course_review> { review };

            mockRepository.Setup(x => x.GetStudentReviews(review.student_id, ref errors)).Returns(reviews);

            //// Act
            reviewService.InsertReview(review, ref errors);

            //// Assert
            mockRepository.Verify(
                x => x.InsertReview(It.IsAny<course_review>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Student reviewed the course already"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RatingIsOutisdeOfBounds()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 11 };

            //// Act
            reviewService.InsertReview(review, ref errors);
        }

        [TestMethod]
        public void StudentDidntTookCourseForReviewing()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 5 };
            var reviews = new List<course_review> { review };

            mockRepository.Setup(x => x.GetStudentReviews(review.student_id, ref errors)).Returns(new List<course_review>());
            mockRepository.Setup(x => x.StudentTookCourse(review.student_id, review.schedule_id, ref errors)).Returns(false);

            //// Act
            reviewService.InsertReview(review, ref errors);

            //// Assert
            mockRepository.Verify(
                x => x.InsertReview(It.IsAny<course_review>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Student can't review a course not taken"));
        }

        [TestMethod]
        public void StudentReviewInserted()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IReviewRepository>();
            var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 10 };

            mockRepository.Setup(x => x.GetStudentReviews(review.student_id, ref errors)).Returns(new List<course_review>());
            mockRepository.Setup(x => x.StudentTookCourse(review.student_id, review.schedule_id, ref errors)).Returns(true);

            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            reviewService.InsertReview(review, ref errors);

            //// Assert
            mockRepository.Verify(x => x.InsertReview(It.IsAny<course_review>(), ref errors), Times.Once);
        }
    }
}