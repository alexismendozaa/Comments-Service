using edit_comment_ms.Data;
using edit_comment_ms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EditCommentMS.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("edit")]
        [Authorize]
        public async Task<IActionResult> EditComment(Guid commentId, string newText)
        {
            var userIdClaim = User.FindFirst("userId"); // Aquí se utiliza el claim personalizado
            if (userIdClaim == null)
            {
                return Unauthorized("No se encontró el identificador de usuario en el token.");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var comment = await _context.Comments
                .Where(c => c.Id == commentId && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                return NotFound("Comentario no encontrado o no tiene permiso para editarlo.");
            }

            comment.Text = newText;
            comment.CreatedAt = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
    }
}
