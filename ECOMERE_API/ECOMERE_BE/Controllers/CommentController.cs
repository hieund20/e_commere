using ECOMERE_BE.Models;
using ECOMERE_BE.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMERE_BE.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly CommentProvider _commentProvider = new CommentProvider();

        public CommentController(ILogger<CommentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("comment")]
        public async Task<JsonResult> GetAllComment()
        {
            try
            {
                var allComment = await _commentProvider.GetAllAsync();
                return new JsonResult(new { 
                    success = true,
                    data = allComment
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

        [HttpPost("comment")]
        public async Task<JsonResult> PostComment([FromBody] Comment comment)
        {
            try
            {
                var newComment = await _commentProvider.AddNewCommentAsync(comment);
                if (newComment != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = newComment
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

        [HttpPut("comment/{id}")]
        public async Task<JsonResult> PutComment(string id, [FromBody] Comment comment)
        {
            try
            {
                var modifiedComment = await _commentProvider.UpdateCommentAsync(id, comment);
                if (modifiedComment != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = modifiedComment
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

        [HttpDelete("comment/{id}")]
        public async Task<JsonResult> DeleteComment(string id)
        {
            try
            {
                var result = await _commentProvider.DeleteCommentAsync(id);
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
