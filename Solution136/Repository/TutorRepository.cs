namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;

    public class TutorRepository : ITutorRepository
    {
        public void InsertTutor(schedule_tutor student, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                context.schedule_tutor.Add(student);
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

        public void UpdateTutor(schedule_tutor student, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result_student = context.schedule_tutor.SingleOrDefault(s =>
                    s.student_id == student.student_id &&
                    s.schedule_id == student.schedule_id);

                if (result_student == null)
                {
                    throw new Exception("Tutor data not found");
                }
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

        public void DeleteTutor(string student_id, int schedule_id, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var student = new schedule_tutor()
                {
                    student_id = student_id,
                    schedule_id = schedule_id
                };

                context.schedule_tutor.Attach(student);
                context.schedule_tutor.Remove(student);
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

        public List<schedule_tutor> GetStudentTutorings(string student_id, ref List<string> errors)
        {
            var context = new cse136Entities();

            var list = new List<schedule_tutor>();

            try
            {
                list = context.schedule_tutor.Include("course_schedule").Where(st => st.student_id == student_id).ToList();
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
    }
}
