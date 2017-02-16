using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Surveys.Web.DAL.SqlServer
{
    public abstract class SqlServerProvider
    {
        public static object GetDataValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        public abstract string ConnectionString { get; set; }

        public virtual Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            Task<int> result;
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();

                result = cmd.ExecuteNonQueryAsync();

                result.ContinueWith((t) =>
                {
                    cmd.Connection = null;
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                    }
                });

                return result;
            }
        }

        public virtual Task<SqlDataReader> ExecuteReaderAsync(string query, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            Task<SqlDataReader> result = null;
            SqlConnection conn = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = commandType;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                result = cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }

            return result;
        }
    }
}