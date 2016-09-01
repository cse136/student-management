namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using IRepository;

    using POCO;

    public class InstructorRepository : BaseRepository, IInstructorRepository
    {

        public void InsertInstructor(instructor instructor, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                context.instructors.Add(instructor);
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

        public void UpdateInstructor(instructor instructor, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result = context.instructors.SingleOrDefault(c =>
                    c.instructor_id == instructor.instructor_id);

                if (result != null)
                {
                    result.first_name = instructor.first_name;
                    result.last_name = instructor.last_name;
                    result.title = instructor.title;
                
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
        
        public List<instructor> GetList(ref List<string> errors, string name = null, string title = null)
        {
            var instructorList = new List<instructor>();
            cse136Entities context = new cse136Entities();

            try
            {
                var query = context.instructors.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.first_name.Contains(name) || c.last_name.Contains(name));
                }

                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(c => c.title.Contains(title));
                }

                instructorList = query.ToList();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }

            return instructorList;
        }


        public instructor GetDetail(int id, ref List<string> errors)
        {
            cse136Entities context = new cse136Entities();

            try
            {
                var instructor = context.instructors.SingleOrDefault(c => c.instructor_id == id);

                return instructor;
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }

            return null;
        }

    }
}
