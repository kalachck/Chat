namespace AspNetChat.Models.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }

        public string ChatName { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
    }
}
