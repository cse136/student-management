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
            var ta = new TeachingAssistant { ta_id = 1, ta_type_id = -1, first = "John", last = "Doe" };

            mockRepository.Setup(x => x.GetTATypes(ref errors)).Returns(new List<TeachingAssistantType>());

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

        //[TestMethod]
        ////[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void DuplicateTAInCourse()
        //{
        //    //// Arranage
        //    var errors = new List<string>();
        //    var mockRepository = new Mock<ITARepository>();
        //    var TAService = new TAService(mockRepository.Object);
        //    var taOriginal = new TeachingAssistant { ta_id = 1, ta_type_id = 1, first = "John", last = "Doe" };
        //    var taDuplicate = new TeachingAssistant { ta_id = 1, ta_type_id = 1, first = "John", last = "Doe" };

        //    try
        //    {
        //        //// Act
        //        TAService.InsertTA(taOriginal, ref errors);
        //        TAService.InsertTA(taDuplicate, ref errors);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        //// Assert
        //        Assert.IsTrue(errors.Contains("Cannot insert duplicate TAs in a course"));
        //    }
        //}

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void UpdateNonexistantTA()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITARepository>();
            var TAService = new TAService(mockRepository.Object);
            var nonexistant_ta_id = Int32.MaxValue;
            var ta = new TeachingAssistant { ta_id = nonexistant_ta_id, ta_type_id = 1, first = "Fake", last = "TA" };

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