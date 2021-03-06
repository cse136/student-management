﻿namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    
    using IRepository;

    using POCO;

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        private const string GetScheduleListProcedure = "spGetScheduleList";

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListProcedure, conn);

                if (year.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@year"].Value = year;
                }

                if (quarter.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 25));
                    adapter.SelectCommand.Parameters["@quarter"].Value = quarter;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()), 
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(), 
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(), 
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(), 
                        Course = new course
                        {
                            course_id = (int)dataSet.Tables[0].Rows[i]["course_id"],
                            course_title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                            course_description = dataSet.Tables[0].Rows[i]["course_description"].ToString(), 
                        }
                    };
                    scheduleList.Add(schedule);
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

            return scheduleList;
        }

        public ICollection<schedule_tutor> GetCourseTutors(int schedule_id, ref List<string> errors)
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

        public void EnrollStudentToCourse(int schedule_id, string student_id, ref List<string> errors)
        {
            var context = new cse136Entities();
            try
            {
                var student_enrollment = new enrollment()
                {
                    schedule_id = schedule_id,
                    student_id = student_id
                };

                context.enrollments.Add(student_enrollment);
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

        public void DropStudentFromCourse(int schedule_id, string student_id, ref List<string> errors)
        {
            var context = new cse136Entities();

            try
            {
                var student_enrollment = new enrollment()
                {
                    schedule_id = schedule_id,
                    student_id = student_id
                };

                context.enrollments.Attach(student_enrollment);
                context.enrollments.Remove(student_enrollment);
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

        public course_schedule GetScheduledCourseDetails(int schedule_id, ref List<string> errors)
        {
            var schedule = new course_schedule();
            var context = new cse136Entities();

            try
            {
                schedule = context.course_schedule.Where(cs => cs.schedule_id == schedule_id).FirstOrDefault();

                if (schedule == null)
                {
                    throw new Exception("Scheduled course not found");
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

            return schedule;
        }
    }
}
