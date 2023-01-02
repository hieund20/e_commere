using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubProductController : ControllerBase
    {
        private readonly ILogger<SubProduct> _logger;
        private readonly SubProductProvider _provider = new SubProductProvider();

        public SubProductController(ILogger<SubProduct> logger)
        {
            _logger = logger;
        }

        [HttpGet("subProduct")]
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

        [HttpGet("subProduct/{id}")]
        public async Task<JsonResult> GetSubSubProduct(string id)
        {
            try
            {
                var item = await _provider.GetSubProductByIDAsync(id);
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

        [HttpPost("subProduct")]
        public async Task<JsonResult> PostSubSubProduct([FromBody] SubProduct subProduct)
        {
            try
            {
                var newProduct = await _provider.AddNewSubProductAsync(subProduct);
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

        [HttpPut("subProduct/{id}")]
        public async Task<JsonResult> PutSubSubProduct(string id, [FromBody] SubProduct subProduct)
        {
            try
            {
                var modifiedProduct = await _provider.UpdateSubProductAsync(id, subProduct);
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


        [HttpDelete("subProduct/{id}")]
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
