namespace Repository
{
    using POCO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TARepository
    {
        public void InsertTA(TeachingAssistant ta, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                if (string.IsNullOrEmpty(ta.first) || string.IsNullOrEmpty(ta.last))
                {
                    throw new Exception("TA's first name and/or last name are null");
                }

                context.TeachingAssistants.Add(ta);
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

        public ICollection<TeachingAssistant> GetCourseTAs(int schedule_id, ref List<string> errors)
        {
            var list = new List<TeachingAssistant>();
            var context = new cse136Entities();

            try
            {
                var course = context.course_schedule.Include("TeachingAssistants")
                    .Where(cs => cs.schedule_id == schedule_id).FirstOrDefault();

                if (course == null)
                {
                    throw new Exception("Course for ID provided doesn't exist");
                }

                list = course.TeachingAssistants.ToList();
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

        public void UpdateTA(TeachingAssistant ta, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result_ta = context.TeachingAssistants.SingleOrDefault(t =>
                    t.ta_id == ta.ta_id);

                if (result_ta == null)
                {
                    throw new Exception("TA for ID provided doesn't exist");
                }

                result_ta.ta_type_id = ta.ta_type_id;
                result_ta.first = ta.first;
                result_ta.last = ta.last;

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

        public void DeleteTA(int ta_id, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var ta = new TeachingAssistant()
                {
                    ta_id = ta_id
                };

                context.TeachingAssistants.Attach(ta);
                context.TeachingAssistants.Remove(ta);
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

        public ICollection<TeachingAssistant> GetTAs(ref List<string> errors)
        {
            var list = new List<TeachingAssistant>();
            var context = new cse136Entities();

            try
            {
                list = context.TeachingAssistants.ToList();
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

        public ICollection<TeachingAssistantType> GetTATypes(ref List<string> errors)
        {
            var list = new List<TeachingAssistantType>();
            var context = new cse136Entities();

            try
            {
                list = context.TeachingAssistantTypes.ToList();
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
 