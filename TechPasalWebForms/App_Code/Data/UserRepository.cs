using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Data
{
    public class UserRepository
    {
        public User GetByEmail(string email)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT UserId, Username, Email, PasswordHash, Role, CreatedAt FROM Users WHERE Email = @Email", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                        return MapUser(r);
                }
            }
            return null;
        }

        public User GetById(int userId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT UserId, Username, Email, PasswordHash, Role, CreatedAt FROM Users WHERE UserId = @UserId", conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return MapUser(r);
                }
            }
            return null;
        }

        public bool EmailExists(string email)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Email = @Email", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public int Register(string username, string email, string password, string role = "Customer")
        {
            var hash = HashPassword(password);
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Users (Username, Email, PasswordHash, Role, CreatedAt) OUTPUT INSERTED.UserId VALUES (@Username, @Email, @Hash, @Role, GETDATE())", conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Hash", hash);
                cmd.Parameters.AddWithValue("@Role", role);
                return (int)cmd.ExecuteScalar();
            }
        }

        public bool ValidatePassword(string plaintext, string storedHash)
        {
            // Format: PBKDF2$iterations$salt$hash
            var parts = storedHash.Split('$');
            if (parts.Length != 4 || parts[0] != "PBKDF2") return false;
            int iterations = int.Parse(parts[1]);
            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] expected = Convert.FromBase64String(parts[3]);
            byte[] actual = GetPbkdf2Bytes(plaintext, salt, iterations, expected.Length);
            return SlowEquals(expected, actual);
        }

        public static string HashPassword(string password)
        {
            int iterations = 10000;
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);
            byte[] hash = GetPbkdf2Bytes(password, salt, iterations, 32);
            return string.Format("PBKDF2${0}${1}${2}", iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                return pbkdf2.GetBytes(outputBytes);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            int diff = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        private User MapUser(SqlDataReader r) => new User
        {
            UserId = (int)r["UserId"],
            Username = r["Username"].ToString(),
            Email = r["Email"].ToString(),
            PasswordHash = r["PasswordHash"].ToString(),
            Role = r["Role"].ToString(),
            CreatedAt = (DateTime)r["CreatedAt"]
        };
    }
}
