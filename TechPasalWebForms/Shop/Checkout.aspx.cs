using System;
using System.Collections.Generic;
using System.Web.Security;
using TechPasalWebForms.Data;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Shop
{
    public partial class Checkout : System.Web.UI.Page
    {
        private decimal _discount = 0;
        private int _couponId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            var cart = new CartRepository();
            if (cart.GetCart().Count == 0) { Response.Redirect("~/Shop/Cart.aspx"); return; }

            if (!IsPostBack)
            {
                LoadSummary();
            }
            else
            {
                if (ViewState["Discount"] != null) _discount = (decimal)ViewState["Discount"];
                if (ViewState["CouponId"] != null) _couponId = (int)ViewState["CouponId"];
            }
        }

        private void LoadSummary()
        {
            var cart = new CartRepository();
            var items = cart.GetCart();
            rptItems.DataSource = items;
            rptItems.DataBind();
            decimal subtotal = cart.GetTotal();
            lblSubtotal.Text = subtotal.ToString("N0");
            lblDiscount.Text = _discount.ToString("N0");
            lblTotal.Text = (subtotal - _discount).ToString("N0");
        }

        protected void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            var repo = new CouponRepository();
            Coupon coupon;
            if (repo.ValidateCoupon(txtCoupon.Text.Trim().ToUpper(), out coupon))
            {
                var cart = new CartRepository();
                _discount = Math.Round(cart.GetTotal() * coupon.DiscountPercent / 100, 2);
                _couponId = coupon.CouponId;
                ViewState["Discount"] = _discount;
                ViewState["CouponId"] = _couponId;
                ViewState["CouponCode"] = coupon.Code;
                lblCouponMsg.Text = string.Format("Coupon applied! {0}% discount.", coupon.DiscountPercent);
                lblCouponMsg.CssClass = "text-success small";
            }
            else
            {
                _discount = 0;
                ViewState["Discount"] = 0m;
                ViewState["CouponId"] = 0;
                lblCouponMsg.Text = "Invalid or expired coupon.";
                lblCouponMsg.CssClass = "text-danger small";
            }
            lblCouponMsg.Visible = true;
            LoadSummary();
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var cart = new CartRepository();
            var items = cart.GetCart();
            if (items.Count == 0) { Response.Redirect("~/Shop/Cart.aspx"); return; }

            int userId = GetCurrentUserId();
            decimal subtotal = cart.GetTotal();
            decimal discount = (ViewState["Discount"] as decimal?) ?? 0m;
            decimal total = subtotal - discount;

            var orderDetails = new List<OrderDetail>();
            foreach (var item in items)
                orderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    Subtotal = item.Subtotal
                });

            var order = new Order
            {
                UserId = userId,
                TotalAmount = total,
                PaymentMethod = rblPayment.SelectedValue,
                ShippingAddress = txtAddress.Text.Trim(),
                CouponCode = ViewState["CouponCode"] as string,
                DiscountAmount = discount,
                OrderDetails = orderDetails
            };

            try
            {
                var orderRepo = new OrderRepository();
                int orderId = orderRepo.CreateOrder(order);

                int couponId = _couponId > 0 ? _couponId : (ViewState["CouponId"] as int? ?? 0);
                if (couponId > 0)
                    new CouponRepository().IncrementUsage(couponId);

                cart.ClearCart();

                string pm = rblPayment.SelectedValue;
                if (pm == "eSewa" || pm == "Khalti")
                {
                    Response.Redirect(string.Format("~/Api/PaymentCallback.aspx?gateway={0}&oid={1}&status=success", pm, orderId));
                }
                else
                {
                    Response.Redirect(string.Format("~/Shop/OrderHistory.aspx?placed={0}", orderId));
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Order failed: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private int GetCurrentUserId()
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
