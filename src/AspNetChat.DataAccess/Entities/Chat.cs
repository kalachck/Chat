using AspNetChat.DataAccess.Entities.Abstract;

namespace AspNetChat.DataAccess.Entities
{
    public class Chat : BaseEntity
    {
        public string ChatName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatorId { get; set; }

        public User User { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
