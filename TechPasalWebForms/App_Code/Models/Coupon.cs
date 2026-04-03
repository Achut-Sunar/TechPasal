namespace TechPasalWebForms.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercent { get; set; }
        public int MaxUses { get; set; }
        public int UsedCount { get; set; }
        public System.DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
    }
}
