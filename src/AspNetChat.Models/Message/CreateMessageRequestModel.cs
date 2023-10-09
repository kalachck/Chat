namespace AspNetChat.Models.Message
{
    public class CreateMessageRequestModel
    {
        public string Content { get; set; } = null!;

        public int UserId { get; set; }

        public int ChatId { get; set; }
    }
}
