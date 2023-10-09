using AspNetChat.DataAccess.Entities.Abstract;

namespace AspNetChat.DataAccess.Entities
{
    public class Chat : BaseEntity
    {
        public string ChatName { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Message>? Messages { get; set; }

        public ICollection<UserChat>? UserChats { get; set; }
    }
}
