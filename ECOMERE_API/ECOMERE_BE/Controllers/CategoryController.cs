using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly CategoryProvider _categoryProvider = new CategoryProvider();

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("category")]
        public async Task<JsonResult> GetAllCategory()
        {
            try
            {
                var allCategory = await _categoryProvider.GetAllAsync();
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

        [HttpGet("category/{id}")]
        public async Task<JsonResult> GetCompany(string id)
        {
            try
            {
                var item = await _categoryProvider.GetCategoryByIDAsync(id);
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

        [HttpPost("category")]
        public async Task<JsonResult> PostCategory([FromBody] Category category)
        {
            try
            {
                var newCategory = await _categoryProvider.AddNewCategoryAsync(category);
                if (newCategory != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = newCategory
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

        [HttpPut("category/{id}")]
        public async Task<JsonResult> PutCategory(string id, [FromBody] Category category)
        {
            try
            {
                var modifiedCategory = await _categoryProvider.UpdateCategoryAsync(id, category);
                if (modifiedCategory != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = modifiedCategory
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

        [HttpDelete("category/{id}")]
        public async Task<JsonResult> DeleteCategory(string id)
        {
            try
            {
                var result = await _categoryProvider.DeleteCategoryAsync(id);
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
