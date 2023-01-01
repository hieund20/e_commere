using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubSubProductController : ControllerBase
    {
        private readonly ILogger<SubSubProduct> _logger;
        private readonly SubSubProductProvider _provider = new SubSubProductProvider();

        public SubSubProductController(ILogger<SubSubProduct> logger)
        {
            _logger = logger;
        }

        [HttpGet("subSubProduct")]
        public async Task<JsonResult> GetAllSubSubProduct()
        {
            try
            {
                var allCategory = await _provider.GetAllAsync();
                return new JsonResult(new
                {
                    success = true,
                    data = allCategory
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

        [HttpGet("subSubProduct/{id}")]
        public async Task<JsonResult> GetSubSubProduct(string id)
        {
            try
            {
                var item = await _provider.GetSubSubProductByIDAsync(id);
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

        [HttpPost("subSubProduct")]
        public async Task<JsonResult> PostSubSubProduct([FromBody] SubSubProduct subSubProduct)
        {
            try
            {
                var newProduct = await _provider.AddNewSubSubProductAsync(subSubProduct);
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
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("subSubProduct/{id}")]
        public async Task<JsonResult> PutSubSubProduct(string id, [FromBody] SubSubProduct subSubProduct)
        {
            try
            {
                var modifiedProduct = await _provider.UpdateSubSubProductAsync(id, subSubProduct);
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

        [HttpPut("subSubProduct/uploadImage{id}")]
        public async Task<JsonResult> PutImageSubSubProduct(string id, IFormFile file)
        {
            try
            {
                var modifiedProduct = await _provider.UpdateImageSubSubProductAsync(id, file);
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

        [HttpDelete("subSubProduct/{id}")]
        public async Task<JsonResult> DeleteSubSubProduct(string id)
        {
            try
            {
                var result = await _provider.DeleteSubSubProductAsync(id);
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
