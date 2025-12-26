using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE355BYS
{
    public partial class ProductPage : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadProducts();
            }
        }

        private void LoadDropdowns()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // Kategoriler
                SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryID, CategoryName FROM Category", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataBind();

                // Para Birimleri
                SqlDataAdapter daCur = new SqlDataAdapter("SELECT CurrencyCode FROM Currency", con);
                DataTable dtCur = new DataTable();
                daCur.Fill(dtCur);
                ddlCurrency.DataSource = dtCur;
                ddlCurrency.DataTextField = "CurrencyCode";
                ddlCurrency.DataValueField = "CurrencyCode";
                ddlCurrency.DataBind();
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 20 ProductID, ProductName, PriceTRY, CurrentStock FROM Product ORDER BY ProductID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        protected void btnRefreshPrices_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // SP ÇALIŞTIRMA
                SqlCommand cmd = new SqlCommand("sp_RefreshProductTryPrice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadProducts();
            lblMsg.Text = "Success: Prices refreshed via Stored Procedure!";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Basit Insert kodu (Hızlıca ekledim)
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string sql = "INSERT INTO Product (CategoryID, ProductName, BasePrice, BaseCurrency, PriceTRY, VATRate, Unit, CurrentStock, IsActive) VALUES (@Cat, @Name, @Price, @Cur, @Price, 18, 'Piece', 0, 1)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Cat", ddlCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtBasePrice.Text));
                cmd.Parameters.AddWithValue("@Cur", ddlCurrency.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadProducts();
            lblMsg.Text = "Product Added!";
        }
    }
}