﻿using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class CartProvider : baseProvider
    {
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        private HttpContext _httpContext => new HttpContextAccessor().HttpContext;

        public async Task<List<Cart>> GetAllAsync()
        {
            try
            {
                var data = await db.Cart
                    .Include(p => p.Product)
                    .OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Cart>();
            }
        }

        public async Task<int> GetTotalPriceAsync()
        {
            try
            {
                int total = 0;
                var data = await db.Cart
                    .Include(p => p.Product)
                    .OrderBy(c => c.CreatedAt).ToListAsync();

                for (int i = 0; i < data.Count; i++)
                {
                    total += (int)(data[i].Quantity * data[i].Product.UnitPrice);
                }

                return total;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> GetCartNumberAsync()
        {
            try
            {
                int number = 0;
                var data = await db.Cart
                    .Include(p => p.Product)
                    .OrderBy(c => c.CreatedAt).ToListAsync();

                for (int i = 0; i < data.Count; i++)
                {
                    number += 1;
                }

                return number;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<Cart> AddNewProductAsync(Cart newCart)
        {
            try
            {
                newCart.Id = Guid.NewGuid().ToString();
                newCart.CreatedAt = DateTime.Now;
                db.Cart.Add(newCart);
                await db.SaveChangesAsync();
                return newCart;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
