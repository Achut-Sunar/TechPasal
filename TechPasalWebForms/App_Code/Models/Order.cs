using System.Collections.Generic;

namespace TechPasalWebForms.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }  // Pending, Processing, Shipped, Delivered, Cancelled
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }  // eSewa, Khalti, CashOnDelivery, BankTransfer
        public string PaymentStatus { get; set; }  // Pending, Paid, Failed
        public string ShippingAddress { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
