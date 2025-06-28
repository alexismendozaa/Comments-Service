using add_comment_ms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Security.Claims;

namespace AddCommentMS.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public CommentsController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        // Endpoint para añadir un comentario
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentDto commentDto)
        {
            var userId = HttpContext.User.FindFirstValue("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("No valid JWT token provided.");
            }

            var commentId = Guid.NewGuid();
            var postIdGuid = Guid.Parse(commentDto.PostId);
            var userIdGuid = Guid.Parse(userId); // Conversión aquí

            var comment = new Comment
            {
                Id = commentId.ToString(),
                PostId = commentDto.PostId,
                UserId = userId,
                Text = commentDto.Text,
                CreatedAt = DateTime.UtcNow
            };

            var command = new NpgsqlCommand("INSERT INTO comments (id, post_id, user_id, text, created_at) VALUES (@id, @post_id, @user_id, @text, @created_at)", _connection);
            command.Parameters.AddWithValue("id", commentId);
            command.Parameters.AddWithValue("post_id", postIdGuid);
            command.Parameters.AddWithValue("user_id", userIdGuid); // Usar Guid aquí
            command.Parameters.AddWithValue("text", comment.Text);
            command.Parameters.AddWithValue("created_at", comment.CreatedAt);

            await _connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return Ok(new { message = "Comment added successfully." });
        }
    }
}