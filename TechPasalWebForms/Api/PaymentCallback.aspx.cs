using System;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Api
{
    public partial class PaymentCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string gateway = Request.QueryString["gateway"] ?? "";
            string status = Request.QueryString["status"] ?? "";
            int orderId;
            if (!int.TryParse(Request.QueryString["oid"], out orderId))
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            var repo = new OrderRepository();
            if (status == "success")
            {
                repo.UpdatePaymentStatus(orderId, "Paid", "Processing");
                Response.Redirect(string.Format("~/Shop/OrderHistory.aspx?placed={0}", orderId));
            }
            else
            {
                repo.UpdatePaymentStatus(orderId, "Failed", "Cancelled");
                Response.Redirect(string.Format("~/Shop/OrderHistory.aspx?failed={0}", orderId));
            }
        }
    }
}
