using System;
using System.Web.UI;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Shop
{
    public partial class Products : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProducts();
            }
        }

        private void LoadCategories()
        {
            var repo = new ProductRepository();
            ddlCategory.DataSource = repo.GetCategories();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Categories", ""));
        }

        private void LoadProducts(string search = null, string category = null)
        {
            var repo = new ProductRepository();
            var products = repo.GetAll(search, category);
            rptProducts.DataSource = products;
            rptProducts.DataBind();
            lblNoProducts.Visible = products.Count == 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim(), ddlCategory.SelectedValue);
        }
    }
}
