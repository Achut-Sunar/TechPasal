using System.Collections.Generic;
using System.Data.SqlClient;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Data
{
    public class ProductRepository
    {
        public List<Product> GetAll(string search = null, string category = null)
        {
            var list = new List<Product>();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM Products WHERE 1=1";
                if (!string.IsNullOrEmpty(search)) sql += " AND (Name LIKE @Search OR Description LIKE @Search)";
                if (!string.IsNullOrEmpty(category)) sql += " AND Category = @Category";
                var cmd = new SqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                if (!string.IsNullOrEmpty(category)) cmd.Parameters.AddWithValue("@Category", category);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapProduct(r));
            }
            return list;
        }

        public List<Product> GetFeatured()
        {
            var list = new List<Product>();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Products WHERE IsFeatured = 1 AND Stock > 0", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapProduct(r));
            }
            return list;
        }

        public Product GetById(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Products WHERE ProductId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var r = cmd.ExecuteReader())
                    if (r.Read()) return MapProduct(r);
            }
            return null;
        }

        public List<string> GetCategories()
        {
            var list = new List<string>();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT DISTINCT Category FROM Products ORDER BY Category", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(r[0].ToString());
            }
            return list;
        }

        public void Create(Product p)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Products (Name, Description, Price, DiscountedPrice, Stock, Category, ImageUrl, IsFeatured, CreatedAt) VALUES (@Name,@Desc,@Price,@DP,@Stock,@Cat,@Img,@Featured,GETDATE())", conn);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Desc", p.Description ?? "");
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@DP", (object)p.DiscountedPrice ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", p.Stock);
                cmd.Parameters.AddWithValue("@Cat", p.Category ?? "");
                cmd.Parameters.AddWithValue("@Img", p.ImageUrl ?? "");
                cmd.Parameters.AddWithValue("@Featured", p.IsFeatured);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product p)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Products SET Name=@Name, Description=@Desc, Price=@Price, DiscountedPrice=@DP, Stock=@Stock, Category=@Cat, ImageUrl=@Img, IsFeatured=@Featured WHERE ProductId=@Id", conn);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Desc", p.Description ?? "");
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@DP", (object)p.DiscountedPrice ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", p.Stock);
                cmd.Parameters.AddWithValue("@Cat", p.Category ?? "");
                cmd.Parameters.AddWithValue("@Img", p.ImageUrl ?? "");
                cmd.Parameters.AddWithValue("@Featured", p.IsFeatured);
                cmd.Parameters.AddWithValue("@Id", p.ProductId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Products WHERE ProductId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private Product MapProduct(SqlDataReader r)
        {
            return new Product
            {
                ProductId = (int)r["ProductId"],
                Name = r["Name"].ToString(),
                Description = r["Description"].ToString(),
                Price = (decimal)r["Price"],
                DiscountedPrice = r["DiscountedPrice"] == System.DBNull.Value ? (decimal?)null : (decimal)r["DiscountedPrice"],
                Stock = (int)r["Stock"],
                Category = r["Category"].ToString(),
                ImageUrl = r["ImageUrl"].ToString(),
                IsFeatured = (bool)r["IsFeatured"],
                CreatedAt = (System.DateTime)r["CreatedAt"]
            };
        }
    }
}
