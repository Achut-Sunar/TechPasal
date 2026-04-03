using System;
using TechPasalWebForms.Data;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Shop
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private Product _product;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(Request.QueryString["id"], out id))
            {
                pnlNotFound.Visible = true;
                pnlProduct.Visible = false;
                return;
            }
            _product = new ProductRepository().GetById(id);
            if (_product == null)
            {
                pnlNotFound.Visible = true;
                pnlProduct.Visible = false;
                return;
            }

            if (!IsPostBack)
            {
                lblName.Text = Server.HtmlEncode(_product.Name);
                lblCategory.Text = Server.HtmlEncode(_product.Category);
                lblDescription.Text = Server.HtmlEncode(_product.Description);
                lblStock.Text = _product.Stock > 0 ? _product.Stock + " available" : "Out of stock";
                imgProduct.Src = _product.ImageUrl;
                if (_product.DiscountedPrice.HasValue)
                {
                    lblPrice.Text = "Rs. " + _product.DiscountedPrice.Value.ToString("N0");
                    lblOriginalPrice.Text = "Rs. " + _product.Price.ToString("N0");
                }
                else
                {
                    lblPrice.Text = "Rs. " + _product.Price.ToString("N0");
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (_product == null) return;
            int qty;
            if (!int.TryParse(txtQty.Text, out qty) || qty < 1) qty = 1;
            var cart = new CartRepository();
            decimal price = _product.DiscountedPrice ?? _product.Price;
            cart.AddOrUpdate(_product.ProductId, _product.Name, price, qty, _product.ImageUrl);
            lblMessage.Text = string.Format("Added {0} item(s) to cart!", qty);
            lblMessage.Visible = true;
        }
    }
}
