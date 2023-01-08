using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly CartProvider _provider = new CartProvider();

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet("cart")]
        public async Task<JsonResult> GetAllCart()
        {
            try
            {
                var allCart = await _provider.GetAllAsync();
                var totalPrice = await _provider.GetTotalPriceAsync();
                var cartNumber = await _provider.GetCartNumberAsync();

                return new JsonResult(new
                {
                    success = true,
                    data = allCart,
                    totalPrice = totalPrice,
                    cartNumber = cartNumber
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

        [HttpPost("cart/addProductToCart")]
        public async Task<JsonResult> AddProductToCart([FromBody] Cart cart)
        {
            try
            {
                var newItem = await _provider.AddNewProductAsync(cart);
                if (newItem != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = newItem
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

        //[HttpPut("comment/{id}")]
        //public async Task<JsonResult> PutComment(string id, [FromBody] Comment comment)
        //{
        //    try
        //    {
        //        var modifiedComment = await _provider.UpdateCommentAsync(id, comment);
        //        if (modifiedComment != null)
        //        {
        //            return new JsonResult(new
        //            {
        //                success = true,
        //                data = modifiedComment
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

        //[HttpDelete("comment/{id}")]
        //public async Task<JsonResult> DeleteComment(string id)
        //{
        //    try
        //    {
        //        var result = await _provider.DeleteCommentAsync(id);
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
