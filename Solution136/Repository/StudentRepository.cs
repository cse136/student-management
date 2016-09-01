namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class StudentRepository : BaseRepository, IStudentRepository
    {
        private const string InsertStudentInfoProcedure = "spInsertStudentInfo";
        private const string UpdateStudentInfoProcedure = "spUpdateStudentInfo";
        private const string DeleteStudentInfoProcedure = "spDeleteStudentInfo";
        private const string GetStudentListProcedure = "spGetStudentList";
        private const string GetStudentInfoProcedure = "spGetStudentInfo";
        private const string InsertStudentScheduleProcedure = "spInsertStudentSchedule";
        private const string DeleteStudentScheduleProcedure = "spDeleteStudentSchedule";

        public void InsertStudent(student student, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertStudentInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ssn", SqlDbType.VarChar, 9));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoe_size", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@weight", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = student.student_id;
                adapter.SelectCommand.Parameters["@ssn"].Value = student.ssn;
                adapter.SelectCommand.Parameters["@first_name"].Value = student.first_name;
                adapter.SelectCommand.Parameters["@last_name"].Value = student.last_name;
                adapter.SelectCommand.Parameters["@email"].Value = student.email;
                adapter.SelectCommand.Parameters["@password"].Value = student.password;
                adapter.SelectCommand.Parameters["@shoe_size"].Value = student.shoe_size;
                adapter.SelectCommand.Parameters["@weight"].Value = student.weight;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void UpdateStudent(student student, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ssn", SqlDbType.VarChar, 9));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoe_size", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@weight", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = student.student_id;
                adapter.SelectCommand.Parameters["@ssn"].Value = student.ssn;
                adapter.SelectCommand.Parameters["@first_name"].Value = student.first_name;
                adapter.SelectCommand.Parameters["@last_name"].Value = student.last_name;
                adapter.SelectCommand.Parameters["@email"].Value = student.email;
                adapter.SelectCommand.Parameters["@password"].Value = student.password;
                adapter.SelectCommand.Parameters["@shoe_size"].Value = student.shoe_size;
                adapter.SelectCommand.Parameters["@weight"].Value = student.weight;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DeleteStudent(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteStudentInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public student GetStudentDetail(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            student student = null;

            try
            {
                var adapter = new SqlDataAdapter(GetStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                student = new student
                              {
                                  student_id = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                                  first_name = dataSet.Tables[0].Rows[0]["first_name"].ToString(),
                                  last_name = dataSet.Tables[0].Rows[0]["last_name"].ToString(),
                                  ssn = dataSet.Tables[0].Rows[0]["ssn"].ToString(),
                                  email = dataSet.Tables[0].Rows[0]["email"].ToString(),
                                  password = dataSet.Tables[0].Rows[0]["password"].ToString(),
                                  shoe_size =
                                      (float)Convert.ToDouble(dataSet.Tables[0].Rows[0]["shoe_size"].ToString()),
                                  weight = Convert.ToInt32(dataSet.Tables[0].Rows[0]["weight"].ToString())
                              };

                if (dataSet.Tables[1] != null)
                {
                    student.enrollments = new List<enrollment>();
                    for (var i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                    {
                        var enrollment = new enrollment();
                        var schedule = new course_schedule();
                        var course = new course
                                         {
                                             course_id = (int)dataSet.Tables[1].Rows[i]["course_id"],
                                             course_title = dataSet.Tables[1].Rows[i]["course_title"].ToString(),
                                             course_description =
                                                 dataSet.Tables[1].Rows[i]["course_description"].ToString()
                                         };
                        schedule.course = course;

                        schedule.quarter = dataSet.Tables[1].Rows[i]["quarter"].ToString();
                        schedule.year = (int)dataSet.Tables[1].Rows[i]["year"];
                        schedule.session = dataSet.Tables[1].Rows[i]["session"].ToString();
                        schedule.schedule_id = Convert.ToInt32(dataSet.Tables[1].Rows[i]["schedule_id"].ToString());
                        enrollment.course_schedule = schedule;
                        student.enrollments.Add(enrollment);
                    }
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

            return student;
        }

        public List<student> GetStudentList(ref List<string> errors, string id = null, string first = null, string last = null, string email = null, double gpa = 0)
        {
            var studentList = new List<student>();
            var context = new cse136Entities();

            try
            {
                var query = context.students.AsQueryable();

                if (!string.IsNullOrEmpty(id))
                {
                    query = query.Where(s => s.student_id == id);
                }

                if (!string.IsNullOrEmpty(first))
                {
                    query = query.Where(s => s.first_name.Contains(first));
                }

                if (!string.IsNullOrEmpty(last))
                {
                    query = query.Where(s => s.last_name.Contains(last));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(s => s.email.Contains(email));
                }

                //if (!string.IsNullOrEmpty(gpa))
               // {
                    //TODO: Get GPA
               // }
                studentList = query.ToList();

            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                context.Dispose();
            }

            return studentList;
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertStudentScheduleProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType
                                                  =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteStudentScheduleProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType
                                                  =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public List<enrollment> GetEnrollments(string student_id, ref List<string> errors)
        {
            var list = new List<enrollment>();
            var context = new cse136Entities();

            try
            {
                list = context.enrollments.Include("course_schedule")
                    .Where(e => e.student_id == student_id)
                    .ToList();
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
