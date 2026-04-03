using System;
using System.Web.Security;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Shop
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["placed"] != null)
                {
                    lblPlaced.Text = string.Format("Order #{0} placed successfully!", Request.QueryString["placed"]);
                    lblPlaced.Visible = true;
                }
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            var repo = new OrderRepository();
            gvOrders.DataSource = repo.GetByUserId(GetUserId());
            gvOrders.DataBind();
        }

        protected void gvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderId = (int)gvOrders.SelectedDataKey.Value;
            var repo = new OrderRepository();
            var order = repo.GetById(orderId);
            if (order != null)
            {
                gvDetails.DataSource = order.OrderDetails;
                gvDetails.DataBind();
                pnlDetails.Visible = true;
            }
        }

        private int GetUserId()
        {
            var identity = User.Identity as FormsIdentity;
            if (identity != null)
            {
                var parts = identity.Ticket.UserData.Split('|');
                int uid;
                if (parts.Length > 0 && int.TryParse(parts[0], out uid)) return uid;
            }
            return 0;
        }
    }
}
