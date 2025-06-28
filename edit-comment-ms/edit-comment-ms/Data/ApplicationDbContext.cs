using edit_comment_ms.Models;
using Microsoft.EntityFrameworkCore;

namespace edit_comment_ms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.PostId).HasColumnName("post_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}