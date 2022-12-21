using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly UserRoleProvider _provider = new UserRoleProvider();

        public UserRoleController(ILogger<UserRoleController> logger)
        {
            _logger = logger;
        }

        [HttpGet("userRole")]
        public async Task<JsonResult> GetAllComment()
        {
            try
            {
                var allUserRole = await _provider.GetAllAsync();
                return new JsonResult(new { 
                    success = true,
                    data = allUserRole
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

        [HttpPost("userRole")]
        public async Task<JsonResult> PostUser([FromBody] UserRole userRole)
        {
            try
            {
                var newUserRole = await _provider.AddNewUserRoleAsync(userRole);
                if (newUserRole != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = newUserRole
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

        [HttpPut("userRole/{id}")]
        public async Task<JsonResult> PutUser(string id, [FromBody] UserRole userRole)
        {
            try
            {
                var modifiedUserRole = await _provider.UpdateUserRoleAsync(id, userRole);
                if (modifiedUserRole != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = modifiedUserRole
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

        [HttpDelete("userRole/{id}")]
        public async Task<JsonResult> DeleteUserRole(string id)
        {
            try
            {
                var result = await _provider.DeleteUserRoleAsync(id);
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
