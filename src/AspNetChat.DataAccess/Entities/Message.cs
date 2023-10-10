using AspNetChat.DataAccess.Entities.Abstract;

namespace AspNetChat.DataAccess.Entities
{
    public class Message : BaseEntity
    {
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }

        public User? User { get; set; }

        public string ChatName { get; set; } = null!;

        public Chat? Chat { get; set; }
    }
}
