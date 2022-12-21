using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class OrderProvider : baseProvider
    {
        public async Task<List<Order>> GetAllAsync()
        {
            try
            {
                var data = await db.Order.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Order>();
            }
        }

        public async Task<Order> GetOrderByIDAsync(string ID)
        {
            return await db.Order.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<Order> AddNewOrderAsync(Order newOrder)
        {
            try
            {
                newOrder.Id = Guid.NewGuid().ToString();
                newOrder.CreatedAt = DateTime.Now;
                db.Order.Add(newOrder);
                await db.SaveChangesAsync();
                return newOrder;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Order> UpdateOrderAsync(string id, Order modifiedOrder)
        {
            try
            {
                Order existingOrder = await db.Order.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingOrder != null)
                {
                    existingOrder.PaymentTransactionId = modifiedOrder.PaymentTransactionId;
                    existingOrder.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingOrder;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            try
            {
                Order existingOrder = await db.Order.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingOrder != null)
                {
                    db.Order.Remove(existingOrder);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
