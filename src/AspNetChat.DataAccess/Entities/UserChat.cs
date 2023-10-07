using AspNetChat.DataAccess.Entities.Abstract;

namespace AspNetChat.DataAccess.Entities
{
    public class UserChat : BaseEntity
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int ChatId { get; set; }

        public Chat? Chat { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
