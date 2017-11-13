using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyQuanAn
{
    class SQLDataHelper
    {
        //Để connection string của bạn ở đây, vui lòng comment (//) connection string của người khác và đừng xóa

        //Hùng
        private static string connectionString = @"Data Source=DESKTOP-87FU5ES;Initial Catalog=QuanLyQuanAn;Integrated Security=True";
        private static SqlConnection connection;
        private static SqlConnection Connection { get => connection; set => connection = value; }

        public SQLDataHelper()
        {
        }

        ~SQLDataHelper()
        {
        }

        public static void Connect()
        {
            try
            {
                if (Connection == null)
                {
                    Connection = new SqlConnection(connectionString);
                }
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                Connection.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static void Disconnect()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string strSql)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = strSql;
                command.CommandType = cmdType;
                int nRow = command.ExecuteNonQuery();
                return nRow;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string strSql, params SqlParameter[] parameters)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = strSql;
                command.CommandType = cmdType;

                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                int nRow = command.ExecuteNonQuery();
                return nRow;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static SqlDataReader GetReader(CommandType cmdType, string strSql, params SqlParameter[] parameters)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = strSql;
                command.CommandType = cmdType;
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static SqlDataReader GetReader(CommandType cmdType, string strSql)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = strSql;
                command.CommandType = cmdType;

                return command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static DataTable Select(CommandType cmdType, string strSql)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = strSql;
                command.CommandType = cmdType;

                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
