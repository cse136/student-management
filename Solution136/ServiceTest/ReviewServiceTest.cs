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
        [ExpectedException(typeof(Exception))]
        public void StudentCantReviewMoreThanOnce()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 5 };
            var reviews = new List<course_review> { review };

            mockRepository.Setup(x => x.GetCourseReviews(1, ref errors)).Returns(reviews);

            try
            {
                //// Act
                reviewService.InsertReview(review, ref errors);
            }
            catch (Exception ex)
            {
                //// Assert
                Assert.AreEqual("Can't insert a review twice", ex.Message);
            }
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

            //// Assert
            mockRepository.Verify(
                x => x.InsertReview(It.IsAny<course_review>(), ref errors), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StudentDidntTookCourseForReviewing()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = string.Empty };

            //// Act
            studentService.InsertStudent(student, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StudentReviewInserted()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IReviewRepository>();
            var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 11 };

            mockRepository.Setup(x => x.InsertReview(review, ref errors)).Verifiable();

            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            reviewService.InsertReview(review, ref errors);

            //// Assert
            mockRepository.Verify(x => x.InsertReview(It.IsAny<course_review>(), ref errors), Times.Once);
        }

    }
}