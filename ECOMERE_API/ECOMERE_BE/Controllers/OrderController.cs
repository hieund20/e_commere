using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderProvider _orderProvider = new OrderProvider();
        private readonly CartProvider _cartProvider = new CartProvider();
        private readonly OrderDetailProvider _orderDetailProvider = new OrderDetailProvider();

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet("order")]
        public async Task<JsonResult> GetAllOrder()
        {
            try
            {
                var allOrder = await _orderProvider.GetAllAsync();
                return new JsonResult(new { 
                    success = true,
                    data = allOrder
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

        [HttpGet("order/{id}")]
        public async Task<JsonResult> GetOrder(string id)
        {
            try
            {
                var item = await _orderProvider.GetOrderByIDAsync(id);
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

        [HttpPost("orderCheckout")]
        public async Task<JsonResult> PostCheckoutOrder([FromBody] Order order)
        {
            try
            {
                var newOrder= await _orderProvider.AddNewOrderAsync(order);
                if (newOrder != null)
                {
                    List<Cart> myOrderList = await _cartProvider.GetAllAsync();
                    for(int i = 0; i < myOrderList.Count; i++)
                    {
                        var myOrderDetail = new OrderDetail();
                        myOrderDetail.OrderId= newOrder.Id;
                        myOrderDetail.ProductId = myOrderList[i].ProductId;
                        myOrderDetail.Quantity = myOrderList[i].Quantity;
                        myOrderDetail.UnitPrice = myOrderList[i].Product.UnitPrice;

                        _orderDetailProvider.AddNewOrderDetailAsync(myOrderDetail);
                    }

                    return new JsonResult(new
                    {
                        success = true,
                        data = newOrder
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        success = false
                    });
                }
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

        [HttpPut("order/{id}")]
        public async Task<JsonResult> PutPrder(string id, [FromBody] Order order)
        {
            try
            {
                var modifiedOrder = await _orderProvider.UpdateOrderAsync(id, order);
                if (modifiedOrder != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = modifiedOrder
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        success = false
                    });
                }
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

        [HttpDelete("order/{id}")]
        public async Task<JsonResult> DeleteOrder(string id)
        {
            try
            {
                var result = await _orderProvider.DeleteOrderAsync(id);
                return new JsonResult(new { success = result });
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
    }
}
