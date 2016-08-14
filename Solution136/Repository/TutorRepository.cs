namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TutorRepository
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

        public void UpdateStudent(schedule_tutor student, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result_student = context.schedule_tutor.SingleOrDefault(s =>
                    s.student_id == student.student_id &&
                    s.course_schedule == student.course_schedule);

                if (result_student != null)
                {
                    result_student.availability_day_id = student.availability_day_id;
                    result_student.availability_time_id = student.availability_time_id;
                    context.SaveChanges();
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

        public void DeleteStudent(string student_id, int schedule_id, ref List<string> errors)
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

        public ICollection<schedule_tutor> GetClassTutors(int schedule_id, ref List<string> errors)
        {
            var list = new List<schedule_tutor>();
            var context = new cse136Entities();
            
            try
            {
                list = context.schedule_tutor.Where(st => st.schedule_id == schedule_id).ToList();
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
