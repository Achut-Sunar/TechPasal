using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Data
{
    public class OrderRepository
    {
        public int CreateOrder(Order order)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new SqlCommand(@"INSERT INTO Orders (UserId, Status, TotalAmount, PaymentMethod, PaymentStatus, ShippingAddress, CouponCode, DiscountAmount, CreatedAt) 
                            OUTPUT INSERTED.OrderId 
                            VALUES (@UserId,@Status,@Total,@PayMethod,@PayStatus,@Ship,@Coupon,@Discount,GETDATE())", conn, tran);
                        cmd.Parameters.AddWithValue("@UserId", order.UserId);
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@Total", order.TotalAmount);
                        cmd.Parameters.AddWithValue("@PayMethod", order.PaymentMethod);
                        cmd.Parameters.AddWithValue("@PayStatus", "Pending");
                        cmd.Parameters.AddWithValue("@Ship", order.ShippingAddress ?? "");
                        cmd.Parameters.AddWithValue("@Coupon", (object)order.CouponCode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Discount", order.DiscountAmount);
                        int orderId = (int)cmd.ExecuteScalar();

                        foreach (var detail in order.OrderDetails)
                        {
                            var dcmd = new SqlCommand(@"INSERT INTO OrderDetails (OrderId, ProductId, ProductName, Quantity, UnitPrice, Subtotal) 
                                VALUES (@OId,@PId,@PName,@Qty,@Price,@Sub)", conn, tran);
                            dcmd.Parameters.AddWithValue("@OId", orderId);
                            dcmd.Parameters.AddWithValue("@PId", detail.ProductId);
                            dcmd.Parameters.AddWithValue("@PName", detail.ProductName);
                            dcmd.Parameters.AddWithValue("@Qty", detail.Quantity);
                            dcmd.Parameters.AddWithValue("@Price", detail.UnitPrice);
                            dcmd.Parameters.AddWithValue("@Sub", detail.Subtotal);
                            dcmd.ExecuteNonQuery();

                            var scmd = new SqlCommand("UPDATE Products SET Stock = Stock - @Qty WHERE ProductId = @PId AND Stock >= @Qty", conn, tran);
                            scmd.Parameters.AddWithValue("@Qty", detail.Quantity);
                            scmd.Parameters.AddWithValue("@PId", detail.ProductId);
                            int rowsAffected = scmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                throw new InvalidOperationException(string.Format("Insufficient stock for product ID {0}.", detail.ProductId));
                        }

                        tran.Commit();
                        return orderId;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Order> GetByUserId(int userId)
        {
            var list = new List<Order>();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Orders WHERE UserId = @UserId ORDER BY CreatedAt DESC", conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapOrder(r));
            }
            return list;
        }

        public List<Order> GetAll()
        {
            var list = new List<Order>();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Orders ORDER BY CreatedAt DESC", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapOrder(r));
            }
            return list;
        }

        public Order GetById(int orderId)
        {
            Order order = null;
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", orderId);
                using (var r = cmd.ExecuteReader())
                    if (r.Read()) order = MapOrder(r);

                if (order != null)
                {
                    order.OrderDetails = new List<OrderDetail>();
                    var dcmd = new SqlCommand("SELECT * FROM OrderDetails WHERE OrderId = @Id", conn);
                    dcmd.Parameters.AddWithValue("@Id", orderId);
                    using (var dr = dcmd.ExecuteReader())
                        while (dr.Read()) order.OrderDetails.Add(MapDetail(dr));
                }
            }
            return order;
        }

        public void UpdateStatus(int orderId, string status)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Orders SET Status = @Status WHERE OrderId = @Id", conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Id", orderId);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePaymentStatus(int orderId, string paymentStatus, string orderStatus)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Orders SET PaymentStatus = @PS, Status = @OS WHERE OrderId = @Id", conn);
                cmd.Parameters.AddWithValue("@PS", paymentStatus);
                cmd.Parameters.AddWithValue("@OS", orderStatus);
                cmd.Parameters.AddWithValue("@Id", orderId);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetTotalOrders()
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                return (int)new SqlCommand("SELECT COUNT(1) FROM Orders", conn).ExecuteScalar();
            }
        }

        public decimal GetTotalRevenue()
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var r = new SqlCommand("SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE PaymentStatus='Paid'", conn).ExecuteScalar();
                return (decimal)r;
            }
        }

        private Order MapOrder(SqlDataReader r) => new Order
        {
            OrderId = (int)r["OrderId"],
            UserId = (int)r["UserId"],
            Status = r["Status"].ToString(),
            TotalAmount = (decimal)r["TotalAmount"],
            PaymentMethod = r["PaymentMethod"].ToString(),
            PaymentStatus = r["PaymentStatus"].ToString(),
            ShippingAddress = r["ShippingAddress"].ToString(),
            CouponCode = r["CouponCode"] == DBNull.Value ? null : r["CouponCode"].ToString(),
            DiscountAmount = (decimal)r["DiscountAmount"],
            CreatedAt = (DateTime)r["CreatedAt"]
        };

        private OrderDetail MapDetail(SqlDataReader r) => new OrderDetail
        {
            OrderDetailId = (int)r["OrderDetailId"],
            OrderId = (int)r["OrderId"],
            ProductId = (int)r["ProductId"],
            ProductName = r["ProductName"].ToString(),
            Quantity = (int)r["Quantity"],
            UnitPrice = (decimal)r["UnitPrice"],
            Subtotal = (decimal)r["Subtotal"]
        };
    }
}
