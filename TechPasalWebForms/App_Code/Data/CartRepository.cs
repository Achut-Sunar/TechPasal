using System.Collections.Generic;
using System.Web;
using TechPasalWebForms.Models;

namespace TechPasalWebForms.Data
{
    public class CartRepository
    {
        private const string CartKey = "TechPasal_Cart";

        public List<CartItem> GetCart()
        {
            var session = HttpContext.Current.Session[CartKey];
            if (session == null) return new List<CartItem>();
            return (List<CartItem>)session;
        }

        public void AddOrUpdate(int productId, string name, decimal price, int qty, string imageUrl)
        {
            var cart = GetCart();
            var item = cart.Find(c => c.ProductId == productId);
            if (item == null)
                cart.Add(new CartItem { ProductId = productId, ProductName = name, Price = price, Quantity = qty, ImageUrl = imageUrl });
            else
                item.Quantity += qty;
            SaveCart(cart);
        }

        public void UpdateQuantity(int productId, int qty)
        {
            var cart = GetCart();
            var item = cart.Find(c => c.ProductId == productId);
            if (item != null)
            {
                if (qty <= 0) cart.Remove(item);
                else item.Quantity = qty;
            }
            SaveCart(cart);
        }

        public void RemoveItem(int productId)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.ProductId == productId);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            HttpContext.Current.Session[CartKey] = new List<CartItem>();
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (var item in GetCart()) total += item.Subtotal;
            return total;
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Current.Session[CartKey] = cart;
        }
    }
}
