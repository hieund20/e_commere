using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ILogger<OrderDetailController> _logger;
        private readonly OrderDetailProvider _orderDetailProvider = new OrderDetailProvider();

        public OrderDetailController(ILogger<OrderDetailController> logger)
        {
            _logger = logger;
        }

        [HttpGet("orderDetail")]
        public async Task<JsonResult> GetAllOrder()
        {
            try
            {
                var allOrderDetail = await _orderDetailProvider.GetAllAsync();
                return new JsonResult(new { 
                    success = true,
                    data = allOrderDetail
                });
            }
            catch(Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("orderDetail/{id}")]
        public async Task<JsonResult> GetOrderDetail(string id)
        {
            try
            {
                var item = await _orderDetailProvider.GetOrderDetailByIDAsync(id);
                return new JsonResult(new
                {
                    success = true,
                    data = item
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("orderDetail/{orderId}")]
        public async Task<JsonResult> GetAllOrderDetailByOrderId(string orderId)
        {
            try
            {
                var item = await _orderDetailProvider.GetAllOrderDetailByOrderIdAsync(orderId);
                return new JsonResult(new
                {
                    success = true,
                    data = item
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        //[HttpPost("orderCheckout")]
        //public async Task<JsonResult> PostCheckoutOrder([FromBody] Order order)
        //{
        //    try
        //    {
        //        var newOrder= await _orderProvider.AddNewOrderAsync(order);
        //        if (newOrder != null)
        //        {
        //            List<Cart> myOrderList = await _cartProvider.GetAllAsync();
        //            for(int i = 0; i < myOrderList.Count; i++)
        //            {
        //                var myOrderDetail = new OrderDetail();
        //                myOrderDetail.OrderId= newOrder.Id;
        //                myOrderDetail.ProductId = myOrderList[i].ProductId;
        //                myOrderDetail.Quantity = myOrderList[i].Quantity;
        //                myOrderDetail.UnitPrice = myOrderList[i].Product.UnitPrice;

        //                _orderDetailProvider.AddNewOrderDetailAsync(myOrderDetail);
        //            }

        //            return new JsonResult(new
        //            {
        //                success = true,
        //                data = newOrder
        //            });
        //        }
        //        else
        //        {
        //            return new JsonResult(new
        //            {
        //                success = false
        //            });
        //        }
        //    } 
        //    catch(Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            success = false,
        //            message = ex.Message
        //        });
        //    }
        //}

        //[HttpPut("order/{id}")]
        //public async Task<JsonResult> PutPrder(string id, [FromBody] Order order)
        //{
        //    try
        //    {
        //        var modifiedOrder = await _orderProvider.UpdateOrderAsync(id, order);
        //        if (modifiedOrder != null)
        //        {
        //            return new JsonResult(new
        //            {
        //                success = true,
        //                data = modifiedOrder
        //            });
        //        }
        //        else
        //        {
        //            return new JsonResult(new
        //            {
        //                success = false
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            success = false,
        //            message = ex.Message
        //        });
        //    }
        //}

        //[HttpDelete("order/{id}")]
        //public async Task<JsonResult> DeleteOrder(string id)
        //{
        //    try
        //    {
        //        var result = await _orderProvider.DeleteOrderAsync(id);
        //        return new JsonResult(new { success = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            success = false,
        //            message = ex.Message
        //        });
        //    }
        //}
    }
}
