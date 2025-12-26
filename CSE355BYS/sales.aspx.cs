using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE355BYS
{
    public partial class SalesPage : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadDropdowns();
                LoadInvoices();
            }
        }

        private void LoadDropdowns()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // Load Customers
                SqlDataAdapter daCust = new SqlDataAdapter("SELECT CustomerID, CustomerName FROM Customer ORDER BY CustomerName", con);
                DataTable dtCust = new DataTable();
                daCust.Fill(dtCust);
                ddlCustomer.DataSource = dtCust;
                ddlCustomer.DataTextField = "CustomerName";
                ddlCustomer.DataValueField = "CustomerID";
                ddlCustomer.DataBind();

                // Load Products
                SqlDataAdapter daProd = new SqlDataAdapter("SELECT ProductID, ProductName FROM Product ORDER BY ProductName", con);
                DataTable dtProd = new DataTable();
                daProd.Fill(dtProd);
                ddlProduct.DataSource = dtProd;
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductID";
                ddlProduct.DataBind();

                RefreshOpenInvoices();
            }
        }

        private void RefreshOpenInvoices()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // Only OPEN invoices
                SqlDataAdapter daInv = new SqlDataAdapter("SELECT SalesInvID, 'INV#' + CAST(SalesInvID as varchar) + ' - ' + CAST(TotalAmountTRY as varchar) + ' TL' as DisplayInv FROM SalesInvoice WHERE Status='OPEN' ORDER BY SalesInvID DESC", con);
                DataTable dtInv = new DataTable();
                daInv.Fill(dtInv);
                ddlOpenInvoices.DataSource = dtInv;
                ddlOpenInvoices.DataTextField = "DisplayInv";
                ddlOpenInvoices.DataValueField = "SalesInvID";
                ddlOpenInvoices.DataBind();
            }
        }

        private void LoadInvoices()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 SalesInvID, InvoiceDate, TotalAmountTRY, Status FROM SalesInvoice ORDER BY SalesInvID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_CreateSalesInvoice", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", ddlCustomer.SelectedValue);
                    cmd.Parameters.AddWithValue("@InvoiceDate", txtDate.Text);
                    cmd.Parameters.AddWithValue("@CurrencyCode", ddlCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@IsCredit", 1);
                    cmd.Parameters.AddWithValue("@DueDate", DateTime.Now.AddDays(30));

                    SqlParameter outParam = new SqlParameter("@SalesInvID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblMsg.Text = "Success! Invoice #" + outParam.Value + " created.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                RefreshOpenInvoices();
                LoadInvoices();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_AddSalesInvoiceItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SalesInvID", ddlOpenInvoices.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductID", ddlProduct.SelectedValue);
                    cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblMsg.Text = "Item added successfully! Stock updated via Trigger.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                LoadInvoices();
                RefreshOpenInvoices();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}