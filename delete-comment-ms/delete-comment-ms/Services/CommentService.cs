using delete_comment_ms.Data;
using delete_comment_ms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace delete_comment_ms.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, Guid userId)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return false; // El comentario no se encontró
            }

            if (comment.UserId != userId)
            {
                return false; // El usuario no puede borrar este comentario
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
