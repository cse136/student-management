namespace ServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class TutorServiceTest
    {
        [TestMethod]
        public void MinimumGPAForTutoringNotMet()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 1, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "C", 
                        student_id = "1",
                        GradeValue = 2, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 1,
                            schedule_day_id = 2,
                            schedule_time_id = 1,
                            schedule_id = 2 
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                     schedule_id = 1, course_id = 1
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>());
    
            //// Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Student has not the minimum GPA for tutoring the class"));
        }

        [TestMethod]
        public void TutorScheduleConflictsWithTutoredCourse()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 1, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 1,
                            schedule_day_id = 2,
                            schedule_time_id = 1,
                            schedule_id = 2 
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor() 
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2016,
                            session = "1"
                        }
                    }        
                });

            //// Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("There is a conflict on the student schedule"));
        }
       
        [TestMethod]
        public void TutorScheduleConflictsWithCourseTaken()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 1, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors))
                .Returns(new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 1,
                            schedule_day_id = 2,
                            schedule_time_id = 1,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor() 
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }         
                });

            //// Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("There is a conflict on the student schedule"));
        }

        [TestMethod]
        public void StudentDidntTookCourseForTutoring()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 1, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule()
                        {
                            course_id = 2,
                            schedule_day_id = 2,
                            schedule_time_id = 2,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor()
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }      
                });

            //// Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Student needs to take class before tutoring"));
        }

        [TestMethod]
        public void CannotTutorAsSameTimeAsCourse()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 2, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>()
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 2,
                            schedule_day_id = 2,
                            schedule_time_id = 2,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor() 
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }
                });

            //// Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Can't tutor at the same time as the class"));
        }

        [TestMethod]
        public void DuplicateTutorForScheduledCourse()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 2, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment()
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule()
                        {
                            course_id = 2,
                            schedule_day_id = 2,
                            schedule_time_id = 2,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 2,
                    schedule_day_id = 2,
                    schedule_time_id = 4,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor() 
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        schedule_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }
                });

            // Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Student already tutoring scheduled course"));
        }

        [TestMethod]
        public void InsertTutorSuccessfuly()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 2, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 2,
                            schedule_day_id = 2,
                            schedule_time_id = 2,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 2,
                    schedule_day_id = 2,
                    schedule_time_id = 4,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor()
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }
                });

            // Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Once);
        }

        [TestMethod]
        public void UpdatedTutorSuccessfully()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 2, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>()
                { 
                    new enrollment()
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule()
                        {
                            course_id = 2,
                            schedule_day_id = 2,
                            schedule_time_id = 2,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 2,
                    schedule_day_id = 2,
                    schedule_time_id = 4,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor() 
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }
                });

            // Act
            tutorService.InsertTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.InsertTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Once);
        }

        [TestMethod]
        public void UpdatedTutorConflictsWithTutoredCourse()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 1, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment()
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 1,
                            schedule_day_id = 2,
                            schedule_time_id = 1,
                            schedule_id = 2 
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor()
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            //// Act
            tutorService.UpdateTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.UpdateTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("There is a conflict on the student schedule"));
        }

        [TestMethod]
        public void UpdatedTutorConflictsWithTakenCourse()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 1, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule()
                        {
                            course_id = 1,
                            schedule_day_id = 2,
                            schedule_time_id = 1,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor() 
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }
                });

            //// Act
            tutorService.UpdateTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.UpdateTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("There is a conflict on the student schedule"));
        }

        [TestMethod]
        public void UpdatedTutorAsSameTimeAsCourse()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 2, availability_time_id = 1, student_id = "1", schedule_id = 1 };

            mockRepositoryStudent.Setup(x => x.GetEnrollments(tutor.student_id, ref errors)).Returns(
                new List<enrollment>() 
                { 
                    new enrollment() 
                    { 
                        schedule_id = 2, 
                        grade = "A", 
                        student_id = "1",
                        GradeValue = 4, 
                        course_schedule = new course_schedule() 
                        {
                            course_id = 2,
                            schedule_day_id = 2,
                            schedule_time_id = 2,
                            schedule_id = 2,
                            year = 2016,
                            session = "1"
                        }
                    }
                });

            mockRepositorySchedule.Setup(x => x.GetScheduledCourseDetails(tutor.schedule_id, ref errors)).Returns(
                new course_schedule()
                {
                    schedule_id = 1,
                    course_id = 1,
                    schedule_day_id = 2,
                    schedule_time_id = 1,
                    year = 2016,
                    session = "1"
                });

            mockRepository.Setup(x => x.GetStudentTutorings(tutor.student_id, ref errors)).Returns(
                new List<schedule_tutor>() 
                {
                    new schedule_tutor()
                    {
                        availability_day_id = 2,
                        availability_time_id = 1,
                        course_schedule = new course_schedule()
                        {
                            year = 2015,
                            session = "1"
                        }
                    }
                });

            //// Act
            tutorService.UpdateTutor(tutor, ref errors);

            // Assert
            mockRepository.Verify(
                x => x.UpdateTutor(It.IsAny<schedule_tutor>(), ref errors), Times.Never);

            Assert.IsTrue(errors.Contains("Can't tutor at the same time as the class"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatedNullTutor()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            schedule_tutor tutor = null;

            // Act
            tutorService.UpdateTutor(tutor, ref errors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatedWithNullStudentId()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            var tutor = new schedule_tutor() { availability_day_id = 2, availability_time_id = 1, student_id = null, schedule_id = 1 };

            // Act
            tutorService.UpdateTutor(tutor, ref errors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteTutorWithNullId()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            // Act
            tutorService.DeleteTutor(null, 1, ref errors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTutoringListWithNullStudentId()
        {
            // Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITutorRepository>();
            var mockRepositoryStudent = new Mock<IStudentRepository>();
            var mockRepositorySchedule = new Mock<IScheduleRepository>();

            var tutorMockRepository = new Mock<TutorService>();

            var tutorService = new TutorService(
                mockRepository.Object,
                mockRepositoryStudent.Object,
                mockRepositorySchedule.Object);

            // Act
            tutorService.UpdateTutor(null, ref errors);
        }
    }
}
