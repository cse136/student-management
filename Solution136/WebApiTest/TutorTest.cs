namespace WebApiTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using POCO;
    using WebApi.Controllers;

    [TestClass]
    public class TutorTest
    {
        [TestMethod]
        public void UpdateTutorTest()
        {
            var tutorController = new TutorController();
            var updatedTutor = new schedule_tutor() { schedule_id = 116, student_id = "A000002", availability_day_id = 2, availability_time_id = 2 };

            var result = tutorController.UpdateTutor(updatedTutor);

            Assert.AreEqual("ok", result);
        }

        [TestMethod]
        public void InsertTutorTest()
        {
            var tutorController = new TutorController();
            var newTutor = new schedule_tutor() { schedule_id = 116, student_id = "A000002", availability_day_id = 1, availability_time_id = 1 };
            var result = tutorController.InsertTutor(newTutor);
            Assert.AreEqual("ok", result);
        }

        [TestMethod]
        public void DeleteTutorTest()
        {
            var tutorController = new TutorController();

            var result = tutorController.DeleteTutor("A000001", 100);
            Assert.AreEqual("ok", result);
        }

        [TestMethod]
        public void GetStudentTutoringsTest()
        {
            var tutorController = new TutorController();
            var result = tutorController.GetStudentTutorings("A000002");
            Assert.IsTrue(result.Count(t => t.schedule_id == 116 && t.student_id == "A000002") == 1);
        }
    }
}
