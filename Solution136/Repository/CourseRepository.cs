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
                list = (List<course>)context.courses.Include("course1").Where(c =>
                                    c.course_id == course_id).Select(c=> c.course1).FirstOrDefault().ToList();
               
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

        public List<course> GetCourseList(ref List<string> errors, string course = null, string level = null)
        {
            var courseList = new List<course>();
            cse136Entities context = new cse136Entities();

            try
            {
                var query = context.courses.AsQueryable();

                if (!string.IsNullOrEmpty(course))
                {
                    query = query.Where(c => c.course_title.Contains(course) || c.course_description.Contains(course));
                }

                if (!string.IsNullOrEmpty(level))
                {
                    query = query.Where(c => c.course_level == level);
                }

                courseList = query.ToList();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }

            return courseList;
        }


        public course GetCourseDetail(int id, ref List<string> errors)
        {
            var course = new course();
            cse136Entities context = new cse136Entities();

            try
            {
                course = context.courses.SingleOrDefault(c => c.course_id == id);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }

            return course;
        }


        public void RemovePrereq(int id, int prereq, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var course = context.courses.Include("course1").Where(c =>
                                    c.course_id == id).FirstOrDefault();

                if (course != null)
                {
                    var prereqCourse = course.course1.Where(c => c.course_id == prereq).FirstOrDefault();

                    if (prereqCourse != null)
                    {
                        course.course1.Remove(prereqCourse);
                        context.SaveChanges();
                    }
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


        public void AddPrereq(int id, int prereq, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var course = context.courses.Include("course1").Where(c =>
                                    c.course_id == id).FirstOrDefault();

                if (course != null)
                {
                    var prereqCourse = course.course1.Where(c => c.course_id == prereq).FirstOrDefault();

                    if (prereqCourse != null)
                    {
                        throw new Exception("Can't add duplicate pre-requisites");

                    } else if (id == prereq) {

                        throw new Exception("Can't add same course as pre-requisite");
                    }
                    else
                    {
                        prereqCourse = new course() { course_id = prereq };
                        context.courses.Attach(prereqCourse);
                        course.course1.Add(prereqCourse);
                        context.SaveChanges();
                    }
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

    }
}
