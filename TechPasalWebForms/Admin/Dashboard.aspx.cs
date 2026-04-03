using System;
using System.Data.SqlClient;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadStats();
        }

        private void LoadStats()
        {
            var orderRepo = new OrderRepository();
            lblOrders.Text = orderRepo.GetTotalOrders().ToString();
            lblRevenue.Text = orderRepo.GetTotalRevenue().ToString("N0");

            var prodRepo = new ProductRepository();
            lblProducts.Text = prodRepo.GetAll().Count.ToString();

            using (var conn = Db.GetConnection())
            {
                conn.Open();
                lblUsers.Text = new SqlCommand("SELECT COUNT(1) FROM Users", conn).ExecuteScalar().ToString();
            }
        }
    }
}
