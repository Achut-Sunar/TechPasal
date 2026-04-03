using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechPasalWebForms.Data;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Admin
{
    public partial class AdminProducts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadProducts();
        }

        private void LoadProducts()
        {
            var repo = new ProductRepository();
            gvProducts.DataSource = repo.GetAll();
            gvProducts.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var repo = new ProductRepository();
            decimal price, discPrice;
            int stock;
            decimal.TryParse(txtPrice.Text, out price);
            int.TryParse(txtStock.Text, out stock);
            decimal? dp = decimal.TryParse(txtDiscPrice.Text, out discPrice) ? (decimal?)discPrice : null;

            var p = new Product
            {
                Name = txtName.Text.Trim(),
                Description = txtDesc.Text.Trim(),
                Price = price,
                DiscountedPrice = dp,
                Stock = stock,
                Category = txtCategory.Text.Trim(),
                ImageUrl = txtImageUrl.Text.Trim(),
                IsFeatured = chkFeatured.Checked
            };

            int productId = int.Parse(hdnProductId.Value);
            if (productId == 0)
            {
                repo.Create(p);
                lblMsg.Text = "Product created.";
            }
            else
            {
                p.ProductId = productId;
                repo.Update(p);
                lblMsg.Text = "Product updated.";
            }
            lblMsg.Visible = true;
            ClearForm();
            LoadProducts();
        }

        protected void btnCancel_Click(object sender, EventArgs e) { ClearForm(); }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pid = int.Parse(e.CommandArgument.ToString());
            var repo = new ProductRepository();
            if (e.CommandName == "EditProduct")
            {
                var p = repo.GetById(pid);
                if (p != null)
                {
                    hdnProductId.Value = p.ProductId.ToString();
                    txtName.Text = p.Name;
                    txtDesc.Text = p.Description;
                    txtPrice.Text = p.Price.ToString();
                    txtDiscPrice.Text = p.DiscountedPrice.HasValue ? p.DiscountedPrice.Value.ToString() : "";
                    txtStock.Text = p.Stock.ToString();
                    txtCategory.Text = p.Category;
                    txtImageUrl.Text = p.ImageUrl;
                    chkFeatured.Checked = p.IsFeatured;
                    lblFormTitle.Text = "Edit Product";
                }
            }
            else if (e.CommandName == "DeleteProduct")
            {
                try
                {
                    repo.Delete(pid);
                    lblMsg.Text = "Product deleted.";
                    lblMsg.Visible = true;
                }
                catch (Exception ex)
                {
                    lblErr.Text = "Cannot delete: " + ex.Message;
                    lblErr.Visible = true;
                }
                LoadProducts();
            }
        }

        private void ClearForm()
        {
            hdnProductId.Value = "0";
            txtName.Text = txtDesc.Text = txtPrice.Text = txtDiscPrice.Text = txtCategory.Text = txtImageUrl.Text = "";
            txtStock.Text = "0";
            chkFeatured.Checked = false;
            lblFormTitle.Text = "Add New Product";
        }
    }
}
