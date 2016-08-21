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
    public class TAServiceTest
    {
        [TestMethod]
        public void WrongTAType()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ITARepository>();
            var TAService = new TAService(mockRepository.Object);
            var ta = new TeachingAssistant { ta_id = Int32.MaxValue, ta_type_id = -1, first = "John", last = "Doe" };

            mockRepository.Setup(x => x.GetTATypes(ref errors)).Returns(new List<TeachingAssistantType>());
            mockRepository.Setup(x => x.GetTAs(ref errors)).Returns(
                new List<TeachingAssistant>()
                {
                    new TeachingAssistant()
                    {
                         ta_id = Int32.MaxValue
                    }
                });

            try
            {
                //// Act
                TAService.InsertTA(ta, ref errors);
            }
            catch (ArgumentException ex)
            {
                //// Assert
                Assert.IsTrue(errors.Contains("Invalid TA type ID"));
            }
        }

        [TestMethod]
        public void DuplicateTAInCourse()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITARepository>();
            var TAService = new TAService(mockRepository.Object);
            var taOriginal = new TeachingAssistant { ta_id = Int32.MaxValue, ta_type_id = 1, first = "John", last = "Doe" };
            var taDuplicate = new TeachingAssistant { ta_id = Int32.MaxValue, ta_type_id = 1, first = "John", last = "Doe" };

            mockRepository.Setup(x => x.GetTAs(ref errors)).Returns(new List<TeachingAssistant>());

            try
            {
                //// Act
                TAService.InsertTA(taOriginal, ref errors);
                TAService.InsertTA(taDuplicate, ref errors);
            }
            catch (ArgumentException ex)
            {
                //// Assert
                Assert.IsTrue(errors.Contains("Cannot insert duplicate TAs in a course"));
            }
        }

        [TestMethod]
        public void UpdateNonexistantTA()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITARepository>();
            var TAService = new TAService(mockRepository.Object);
            var nonexistant_ta_id = Int32.MaxValue;
            var ta = new TeachingAssistant { ta_id = nonexistant_ta_id, ta_type_id = 1, first = "Fake", last = "TA" };

            mockRepository.Setup(x => x.GetTAs(ref errors)).Returns(new List<TeachingAssistant>());

            try
            {
                //// Act
                TAService.UpdateTA(ta, ref errors);
            }
            catch (ArgumentException ex)
            {
                //// Assert
                Assert.IsTrue(errors.Contains("TA does not exist"));
            }
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void StudentReviewInserted()
        //{
        //    //// Arrange
        //    var errors = new List<string>();

        //    var mockRepository = new Mock<IReviewRepository>();
        //    var review = new course_review { student_id = "1", schedule_id = 1, comments = "Great class!", rating = 11 };

        //    mockRepository.Setup(x => x.InsertReview(review, ref errors)).Verifiable();

        //    var reviewService = new ReviewService(mockRepository.Object);

        //    //// Act
        //    reviewService.InsertReview(review, ref errors);

        //    //// Assert
        //    mockRepository.Verify(x => x.InsertReview(It.IsAny<course_review>(), ref errors), Times.Once);
        //}

    }
}