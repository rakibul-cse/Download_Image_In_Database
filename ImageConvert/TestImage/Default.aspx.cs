using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using Delve;

namespace TestImage
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void databaseToFileButton_Click(object sender, EventArgs e)
        {
            const string backupDrive = "D:\\FileSystemTest";
            if (!Directory.Exists(backupDrive))
            {
                Directory.CreateDirectory(backupDrive);
            }
            try
            {
                
                //var student = .SP_GetStudentIdAndImage();
                DataTable dtSource = null;
                using (SqlConnection conn = new SqlConnection(DataManager.OraConnString()))
                {
                    SqlCommand sqlComm = new SqlCommand("SP_Studentimg", conn); /// Select Query...
                    //sqlComm.Parameters.AddWithValue("@StudentID", StudentID);
                    //sqlComm.Parameters.AddWithValue("@Year", Year);
                    //sqlComm.Parameters.AddWithValue("@Session", SessionID);
                    //sqlComm.Parameters.AddWithValue("@ExamTitle", ExamTitleID);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = sqlComm;
                    da.Fill(ds, "SP_Studentimg");
                    dtSource = ds.Tables["SP_Studentimg"];
                   // return dtRetak;
                }

                foreach (DataRow s in dtSource.Rows)
                {
                    if (s["StudentImage"] != null)
                    {
                        File.WriteAllBytes("D:\\FileSystemTest\\" + s["StudentImage"].ToString() + ".jpg",
                            (byte[]) s["StudentImage"]);

                        int id = Convert.ToInt32(s["StudentImage"]);
                        string path = "D:\\FileSystemTest\\" + s["StudentImage"].ToString() + ".jpg";

                        var connectionString = WebConfigurationManager.ConnectionStrings["IFB_DB"].ToString();
                        var connection = new SqlConnection(connectionString);
                        var command = new SqlCommand("SP_UpdateStudentForImage", connection) // Update Picture Path....
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.Add(new SqlParameter("@Id", id));
                        command.Parameters.Add(new SqlParameter("@Path", path));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
