namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using IRepository;
    using POCO;

    public class AuthorizeRepository : BaseRepository, IAuthorizeRepository
    {
        private const string GetLoginInfoProcedure = "spGetLoginInfo";

        public Logon Authenticate(string email, string password, ref List<string> errors)
        {
            var logon = new Logon { Id = string.Empty, Role = "invalid" };

            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(GetLoginInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));

                adapter.SelectCommand.Parameters["@email"].Value = email;
                adapter.SelectCommand.Parameters["@password"].Value = password;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    logon.Role = dataSet.Tables[0].Rows[0]["role"].ToString();
                    logon.Id = dataSet.Tables[0].Rows[0]["id"].ToString();
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

            return logon;
        }

        [ApplicationExceptionHandlerAspect]
        public Logon AuthenticateFailedNoStoredProcedure(string email, string password, ref List<string> errors)
        {
            var logon = new Logon { Id = string.Empty, Role = "invalid" };
            var conn = new SqlConnection(ConnectionString);
            var adapter = new SqlDataAdapter("Missing_Stored_Procedure_Name_Goes_Here", conn)
            {
                SelectCommand =
                {
                    CommandType = CommandType.StoredProcedure
                }
            };
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));

            adapter.SelectCommand.Parameters["@email"].Value = email;
            adapter.SelectCommand.Parameters["@password"].Value = password;

            var dataSet = new DataSet();
            adapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                logon.Role = dataSet.Tables[0].Rows[0]["role"].ToString();
                logon.Id = dataSet.Tables[0].Rows[0]["id"].ToString();
            }
            conn.Dispose();

            return logon;
        }
    }
}
