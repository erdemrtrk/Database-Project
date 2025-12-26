using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace CSE355BYS
{
    public partial class _default : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardView();
            }
        }

        private void LoadDashboardView()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // FIX: Removed "ORDER BY TotalValueTRY" to prevent the crash
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 20 * FROM vw_StockOnHand", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvStockView.DataSource = dt;
                gvStockView.DataBind();
            }
        }
    }
}