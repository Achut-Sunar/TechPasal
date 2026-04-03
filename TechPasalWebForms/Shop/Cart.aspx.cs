using System;
using System.Web.UI.WebControls;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Shop
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadCart();
        }

        private void LoadCart()
        {
            var repo = new CartRepository();
            var items = repo.GetCart();
            if (items.Count == 0)
            {
                pnlCart.Visible = false;
                pnlEmpty.Visible = true;
            }
            else
            {
                rptCart.DataSource = items;
                rptCart.DataBind();
                lblTotal.Text = repo.GetTotal().ToString("N0");
            }
        }

        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int productId = int.Parse(e.CommandArgument.ToString());
            var repo = new CartRepository();
            if (e.CommandName == "Remove")
            {
                repo.RemoveItem(productId);
            }
            else if (e.CommandName == "Update")
            {
                var txtQty = (TextBox)e.Item.FindControl("txtQty");
                int qty;
                if (int.TryParse(txtQty.Text, out qty))
                    repo.UpdateQuantity(productId, qty);
            }
            LoadCart();
        }
    }
}
