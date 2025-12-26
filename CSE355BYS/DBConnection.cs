using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CSE355BYS  // <-- Bak burayı CSE355BYS yaptık, artık herkes bunu görecek!
{
    public class DBConnection
    {
        string connectionString;
        SqlConnection con;

        public DBConnection()
        {
            // Web.config içindeki "conStr" ismini kullandığından emin ol
            connectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            con = new SqlConnection(connectionString);
        }

        public DataSet getSelect(string sqlstr)
        {
            try
            {
                // Bağlantıyı güvenli açıp kapatalım
                DataSet ds = new DataSet();
                using (SqlConnection tempCon = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlstr, tempCon))
                    {
                        da.Fill(ds);
                    }
                }
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool execute(string sqlstr)
        {
            try
            {
                using (SqlConnection tempCon = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlstr, tempCon))
                    {
                        tempCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Bağlantı cümlesini dışarıdan almak istersen
        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}