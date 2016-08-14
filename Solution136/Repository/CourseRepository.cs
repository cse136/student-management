namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using IRepository;

    using POCO;

    public class CourseRepository : BaseRepository, ICourseRepository
    {
        private const string GetCourseListProcedure = "spGetCourseList";

        public List<Course> GetCourseList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var courseList = new List<Course>();

            try
            {
                var adapter = new SqlDataAdapter(GetCourseListProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          }
                                  };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var course = new Course
                                     {
                                         CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                                         Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                                         CourseLevel =
                                             (CourseLevel)
                                             Enum.Parse(
                                                 typeof(CourseLevel),
                                                 dataSet.Tables[0].Rows[i]["course_level"].ToString()),
                                         Description = dataSet.Tables[0].Rows[i]["course_description"].ToString()
                                     };
                    courseList.Add(course);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return courseList;
        }

        public void InsertCourse(course course, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                context.courses.Add(course);
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

        public void UpdateCourse(course course, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result_course = context.courses.SingleOrDefault(c =>
                    c.course_id == course.course_id);

                if (result_course != null)
                {
                    result_course.course_description = course.course_description;
                    result_course.course_level = course.course_level;
                    result_course.course_title = course.course_title;
                
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

        public void DeleteCourse(int course_id, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var course = new course()
                {
                    course_id = course_id
                };

                context.courses.Attach(course);
                context.courses.Remove(course);
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

        public void InsertCoursePrereq(int course_id, int prereq_course_id, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var result_course = context.courses.SingleOrDefault(c =>
                    c.course_id == course_id);

                var prereq = new course { course_id = prereq_course_id };

                if (result_course != null)
                {
                    result_course.courses.Add(prereq);
                }

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

        public void DeleteCoursePrereq(int course_id, int prereq_course_id, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var result_course = context.courses.SingleOrDefault(c =>
                    c.course_id == course_id);

                var prereq = new course { course_id = prereq_course_id };

                if (result_course != null)
                {
                    result_course.courses.Remove(prereq);
                }

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

        public ICollection<course> GetCoursePrereqs(int course_id, ref List<string> errors)
        {
            var list = new List<course>();
            var context = new cse136Entities();

            try
            {
                var result_course = context.courses.SingleOrDefault(c =>
                                    c.course_id == course_id);

                list = result_course.courses.ToList();
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
