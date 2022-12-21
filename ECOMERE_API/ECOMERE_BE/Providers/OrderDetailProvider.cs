using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class OrderDetailProvider : baseProvider
    {
        public async Task<List<OrderDetail>> GetAllAsync()
        {
            try
            {
                var data = await db.OrderDetail.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<OrderDetail>();
            }
        }

        public async Task<List<OrderDetail>> GetAllOrderDetailByOrderIdAsync(string orderId)
        {
            try
            {
                var data = await db.OrderDetail.Where(p => p.OrderId == orderId).OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<OrderDetail>();
            }
        }

        public async Task<OrderDetail> GetOrderDetailByIDAsync(string ID)
        {
            return await db.OrderDetail.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> AddNewOrderDetailAsync(OrderDetail newOrderDetail)
        {
            try
            {
                newOrderDetail.Id = Guid.NewGuid().ToString();
                newOrderDetail.CreatedAt = DateTime.Now;
                db.OrderDetail.Add(newOrderDetail);
                await db.SaveChangesAsync();
                return newOrderDetail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public async Task<Order> UpdateOrderAsync(string id, Order modifiedOrder)
        //{
        //    try
        //    {
        //        Order existingOrder = await db.Order.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        //        if (existingOrder != null)
        //        {
        //            existingOrder.PaymentTransactionId = modifiedOrder.PaymentTransactionId;
        //            existingOrder.ModifiedAt = DateTime.Now;
        //            await db.SaveChangesAsync();
        //            return existingOrder;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<bool> DeleteOrderAsync(string id)
        //{
        //    try
        //    {
        //        Order existingOrder = await db.Order.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        //        if (existingOrder != null)
        //        {
        //            db.Order.Remove(existingOrder);
        //            await db.SaveChangesAsync();
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
