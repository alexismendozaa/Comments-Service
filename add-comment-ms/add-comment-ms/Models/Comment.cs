namespace add_comment_ms.Models
{
    public class Comment
    {
        public string Id { get; set; } // UUID
        public string PostId { get; set; } // UUID
        public string UserId { get; set; } // UUID
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
