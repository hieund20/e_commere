using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class CartProvider : baseProvider, IDisposable
    {
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        private HttpContext _httpContext => new HttpContextAccessor().HttpContext;

        public async Task<List<Cart>> GetAllAsync()
        {
            try
            {
                ShoppingCartId = GetCartId();
                var data = await db.Cart.Where(c => c.SessionCartId == ShoppingCartId)
                    .Include(p => p.Product)
                    .OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Cart>();
            }
        }


        public string GetCartId()
        {
            if (_httpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(_httpContext.User.Identity.Name))
                {
                    string name = _httpContext.User.Identity.Name;
                    _httpContext.Session.SetString(CartSessionKey,name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    _httpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return _httpContext.Session.GetString(CartSessionKey).ToString();
        }

        public async Task<Cart> AddProductToCartAsync(string productId)
        {
            try
            {
                // Retrieve the product from the database.           
                ShoppingCartId = GetCartId();
                var cartItem = db.Cart.SingleOrDefault(c => c.SessionCartId == ShoppingCartId
                                && c.ProductId == productId);

                if (cartItem == null)
                {
                    cartItem = new Cart
                    {
                        Id = Guid.NewGuid().ToString(),
                        SessionCartId = ShoppingCartId,
                        ProductId= productId,
                        Product = db.Product.SingleOrDefault(p => p.Id == productId),
                        Quantity = 1,
                        CreatedAt= DateTime.Now
                    };
                    db.Cart.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity++;
                }
                await db.SaveChangesAsync();
                return cartItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }
    }
}
