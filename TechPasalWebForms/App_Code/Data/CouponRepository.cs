using System;
using System.Data.SqlClient;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Data
{
    public class CouponRepository
    {
        public Coupon GetByCode(string code)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Coupons WHERE Code = @Code AND IsActive = 1", conn);
                cmd.Parameters.AddWithValue("@Code", code);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                        return new Coupon
                        {
                            CouponId = (int)r["CouponId"],
                            Code = r["Code"].ToString(),
                            DiscountPercent = (decimal)r["DiscountPercent"],
                            MaxUses = (int)r["MaxUses"],
                            UsedCount = (int)r["UsedCount"],
                            ExpiresAt = r["ExpiresAt"] == DBNull.Value ? (DateTime?)null : (DateTime)r["ExpiresAt"],
                            IsActive = (bool)r["IsActive"]
                        };
                }
            }
            return null;
        }

        public bool ValidateCoupon(string code, out Coupon coupon)
        {
            coupon = GetByCode(code);
            if (coupon == null) return false;
            if (coupon.ExpiresAt.HasValue && coupon.ExpiresAt.Value < DateTime.Now) return false;
            if (coupon.UsedCount >= coupon.MaxUses) return false;
            return true;
        }

        public void IncrementUsage(int couponId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Coupons SET UsedCount = UsedCount + 1 WHERE CouponId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", couponId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
