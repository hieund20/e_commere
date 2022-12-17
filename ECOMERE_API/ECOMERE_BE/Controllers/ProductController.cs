using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductProvider _productProvider = new ProductProvider();

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet("product")]
        public async Task<JsonResult> GetAllProduct()
        {
            try
            {
                var allCategory = await _productProvider.GetAllAsync();
                return new JsonResult(new { 
                    success = true,
                    data = allCategory
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

        [HttpGet("product/{id}")]
        public async Task<JsonResult> GetCompany(string id)
        {
            try
            {
                var item = await _productProvider.GetProductByIDAsync(id);
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

        [HttpPost("product")]
        public async Task<JsonResult> PostProduct([FromBody] Product product)
        {
            try
            {
                var newProduct= await _productProvider.AddNewProductAsync(product);
                if (newProduct != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = newProduct
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

        [HttpPut("product/{id}")]
        public async Task<JsonResult> PutProduct(string id, [FromBody] Product product)
        {
            try
            {
                var modifiedProduct = await _productProvider.UpdateProductAsync(id, product);
                if (modifiedProduct != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = modifiedProduct
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

        [HttpDelete("product/{id}")]
        public async Task<JsonResult> DeleteProduct(string id)
        {
            try
            {
                var result = await _productProvider.DeleteProductAsync(id);
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
