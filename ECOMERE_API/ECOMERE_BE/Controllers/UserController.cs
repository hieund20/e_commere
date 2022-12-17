using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserProvider _userProvider = new UserProvider();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("user")]
        public async Task<JsonResult> GetAllComment()
        {
            try
            {
                var allUser = await _userProvider.GetAllAsync();
                return new JsonResult(new { 
                    success = true,
                    data = allUser
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

        [HttpPost("user")]
        public async Task<JsonResult> PostUser([FromBody] User user)
        {
            try
            {
                var newUser = await _userProvider.AddNewUserAsync(user);
                if (newUser != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = newUser
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

        [HttpPut("user/{id}")]
        public async Task<JsonResult> PutUser(string id, [FromBody] User user)
        {
            try
            {
                var modifiedUser = await _userProvider.UpdateUserAsync(id, user);
                if (modifiedUser != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = modifiedUser
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

        [HttpDelete("user/{id}")]
        public async Task<JsonResult> DeleteUser(string id)
        {
            try
            {
                var result = await _userProvider.DeleteUserAsync(id);
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
