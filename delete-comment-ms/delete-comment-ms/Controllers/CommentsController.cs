using delete_comment_ms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace delete_comment_ms.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteComment([FromQuery] Guid commentId)
        {
            // Extraer el userId del token JWT usando ClaimsPrincipal
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("Invalid or missing userId claim in token");
            }

            var result = await _commentService.DeleteCommentAsync(commentId, userId);

            if (!result)
            {
                return NotFound("Comment not found or user not authorized to delete");
            }

            return Ok("Comment deleted successfully");
        }
    }
}