using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
            private set { _instance = value; }
        }

        public string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyQuanTraSua;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                string[] listParams = query.Split(' ');
                int i = 0;
                foreach (var param in listParams)
                {
                    if (param.Contains('@'))
                    {
                        command.Parameters.AddWithValue(param.Replace(','.ToString(), ""), parameters[i]);
                        i++;
                    }
                }
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            connection.Close();

            return dataTable;
        }

        public int ExecuteNonQuery(string query, object[] parameters = null)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                string[] listParams = query.Split(' ');
                int i = 0;
                foreach (var param in listParams)
                {
                    if (param.Contains('@'))
                    {
                        command.Parameters.AddWithValue(param.Replace(','.ToString(), ""), parameters[i]);
                        i++;
                    }
                }
            }
            result = command.ExecuteNonQuery();
            connection.Close();

            return result;
        }
    }
}
