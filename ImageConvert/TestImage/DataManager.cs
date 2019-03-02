using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

namespace Delve
{
    public class DataManager
    {

        public static string OraConnString()
        {
             return String.Format("Server=.;Database=Pic_DB;User ID=sa;Password=123;pooling = false; Trusted_Connection=False;");

        }

        public static void ExecuteNonQuery(string ConnectionString, string query)
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                }
            }
        }
        internal static string ExecuteScalar(string connectionString, string query)
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myConnection.Open();
                    return myCommand.ExecuteScalar().ToString();
                }
            }
        }
        public static DataTable ExecuteQuery(string ConnectionString, string query, string tableName)
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter myAdapter = new SqlDataAdapter(query, myConnection))
                {
                    DataSet ds = new DataSet();
                    myAdapter.Fill(ds, tableName);
                    ds.Tables[0].TableName = tableName;
                    return ds.Tables[0];
                }
            }
        }
        
    }

}