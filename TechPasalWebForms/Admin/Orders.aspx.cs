using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Admin
{
    public partial class Orders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadOrders();
        }

        private void LoadOrders()
        {
            var repo = new OrderRepository();
            var orders = repo.GetAll();
            gvOrders.DataSource = orders;
            gvOrders.DataBind();
            // Set current status in dropdowns
            for (int i = 0; i < gvOrders.Rows.Count; i++)
            {
                var ddl = (DropDownList)gvOrders.Rows[i].FindControl("ddlStatus");
                if (ddl != null) ddl.SelectedValue = orders[i].Status;
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int orderId = int.Parse(e.CommandArgument.ToString());
                int rowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;
                var ddl = (DropDownList)gvOrders.Rows[rowIndex].FindControl("ddlStatus");
                if (ddl != null)
                {
                    new OrderRepository().UpdateStatus(orderId, ddl.SelectedValue);
                    lblMsg.Text = string.Format("Order #{0} updated to {1}.", orderId, ddl.SelectedValue);
                    lblMsg.Visible = true;
                }
                LoadOrders();
            }
        }
    }
}
