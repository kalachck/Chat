namespace AspNetChat.Models.Message
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public int ChatId { get; set; }
    }
}
