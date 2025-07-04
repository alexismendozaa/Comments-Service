using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Dapper;
using System.Data;

[Route("api/comments")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly DatabaseConfig _databaseConfig;

    public CommentsController(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    // Endpoint para obtener los comentarios de un post junto con la información del usuario
    [HttpGet("list")]
    [Authorize]
    public async Task<IActionResult> GetComments([FromQuery] Guid postId)
    {
        // 1. Obtener los comentarios del post desde la base de datos de comentarios
        using var commentsConn = _databaseConfig.GetCommentsConnection();
        await commentsConn.OpenAsync();

        var commentsQuery = @"
            SELECT 
                id AS Id, 
                text AS Text, 
                created_at AS CreatedAt, 
                user_id AS UserId 
            FROM comments 
            WHERE post_id = @PostId";
        var comments = (await commentsConn.QueryAsync<CommentDto>(commentsQuery, new { PostId = postId })).ToList();

        // 2. Obtener los user_id únicos de los comentarios
        var userIds = comments.Select(c => c.UserId).Distinct().ToArray();

        // 3. Obtener la información de los usuarios desde la base de datos de usuarios
        Dictionary<Guid, UserDto> usersDict = new();
        if (userIds.Length > 0)
        {
            using var usersConn = _databaseConfig.GetUsersConnection();
            await usersConn.OpenAsync();

            var usersQuery = @"
                SELECT 
                    id AS Id, 
                    username AS Username, 
                    email AS Email, 
                    profileimage AS ProfileImage
                FROM ""Users"" 
                WHERE id = ANY(@UserIds)";
            var users = await usersConn.QueryAsync<UserDto>(usersQuery, new { UserIds = userIds });
            usersDict = users.ToDictionary(u => u.Id, u => u);
        }

        // 4. Unir la información y devolver la respuesta
        var result = comments.Select(c =>
        {
            usersDict.TryGetValue(c.UserId, out var user);
            return new
            {
                id = c.Id,
                text = c.Text,
                createdAt = c.CreatedAt,
                user = user == null ? null : new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    profileImage = user.ProfileImage
                }
            };
        });

        return Ok(result);
    }

    // DTOs internos para mapear los resultados de las consultas
    private class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }

    private class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
    }
}