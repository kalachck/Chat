namespace AspNetChat.Models.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }

        public string ChatName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatorId { get; set; }
    }
}
