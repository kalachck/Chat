using AspNetChat.DataAccess.Entities.Abstract;

namespace AspNetChat.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Surname { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Chat>? Chats { get; set; }

        public ICollection<Message>? Messages { get; set; }

        public ICollection<UserChat>? UserChats { get; set; } 
    }
}
