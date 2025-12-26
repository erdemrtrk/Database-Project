using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE355BYS
{
    public partial class ReturnPage : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                txtReturnDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void btnCreateReturn_Click(object sender, EventArgs e)
        {
            try
            {
                // Önce bu fatura kime ait onu bulalım
                int customerId = 0;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string sql = "SELECT CustomerID FROM SalesInvoice WHERE SalesInvID=" + txtReturnSalesInvID.Text;
                    SqlCommand cmdCheck = new SqlCommand(sql, con);
                    var result = cmdCheck.ExecuteScalar();
                    if (result != null) customerId = Convert.ToInt32(result);
                    else
                    {
                        lblReturnMsg.Text = "Hata: Böyle bir fatura bulunamadı!";
                        lblReturnMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                // Şimdi İade Prosedürünü Çağıralım
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_CreateSalesReturn", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SalesInvID", int.Parse(txtReturnSalesInvID.Text));
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@ReturnDate", txtReturnDate.Text);
                    cmd.Parameters.AddWithValue("@Reason", txtReason.Text);

                    SqlParameter outParam = new SqlParameter("@SalesReturnID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblReturnMsg.Text = "Başarılı! İade Kaydı No: " + outParam.Value;
                    lblReturnMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblReturnMsg.Text = "Hata: " + ex.Message;
                lblReturnMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}