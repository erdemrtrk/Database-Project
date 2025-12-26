using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE355BYS
{
    public partial class PaymentPage : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadCustomers();
                LoadPayments();
            }
        }

        private void LoadCustomers()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerID, CustomerName FROM Customer", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlCustomerPay.DataSource = dt;
                ddlCustomerPay.DataTextField = "CustomerName";
                ddlCustomerPay.DataValueField = "CustomerID";
                ddlCustomerPay.DataBind();
            }
        }

        private void LoadPayments()
        {
            // Son 10 ödemeyi listele (TransactionType = 'PAYMENT' olanlar)
            // Not: InventoryTransaction tablosu stok içindir, ödemeler genelde 'CustomerLedger' veya benzeri yerde tutulur. 
            // Ancak hocanın yapısına göre burada basit bir gösterim yapalım.
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    // Stored Procedure: sp_RecordCustomerPayment
                    SqlCommand cmd = new SqlCommand("sp_RecordCustomerPayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Eğer Fatura ID boşsa NULL gönder
                    if (string.IsNullOrWhiteSpace(txtInvID.Text))
                        cmd.Parameters.AddWithValue("@SalesInvID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@SalesInvID", int.Parse(txtInvID.Text));

                    cmd.Parameters.AddWithValue("@CustomerID", ddlCustomerPay.SelectedValue);
                    cmd.Parameters.AddWithValue("@PaymentDate", txtRateDate.Text);
                    cmd.Parameters.AddWithValue("@CurrencyCode", ddlPayCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@AmountForeign", decimal.Parse(txtAmountForeign.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblPayMsg.Text = "Ödeme Başarıyla Kaydedildi!";
                    lblPayMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblPayMsg.Text = "Hata: " + ex.Message;
                lblPayMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}