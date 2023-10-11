namespace AspNetChat.Models.Message
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public string ChatName { get; set; }
    }
}
