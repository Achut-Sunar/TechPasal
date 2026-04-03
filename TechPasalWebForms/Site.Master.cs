using System;
using System.Web.UI;
using TechPasalWebForms.Data;

namespace TechPasalWebForms
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    pnlLoggedIn.Visible = true;
                    pnlLoggedOut.Visible = false;
                    lblUsername.Text = User.Identity.Name;
                    if (User.IsInRole("Admin"))
                        pnlAdmin.Visible = true;
                }

                var cart = new CartRepository();
                lblCartCount.Text = cart.GetCart().Count.ToString();
            }
        }
    }
}
