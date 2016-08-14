namespace Repository
{
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

        public void UpdateTA(TeachingAssistant ta, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result_ta = context.TeachingAssistants.SingleOrDefault(t =>
                    t.ta_id == TeachingAssistant.ta_id );

                if (result_ta != null)
                {
                    result_ta.ta_type_id = ta.ta_type_id;
                    result_ta.first = ta.first;
                    result_ta.last = ta.last;
                    
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
                context.schedule_tutor.Remove(ta);
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
                list = context.TeachingAssistants.Include("course_schedule").Where(t => t.course_schedule.schedule_id == schedule_id).ToList();
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
