namespace AspNetChat.Models.Chat
{
    public class CreateChatRequestModel
    {
        public string ChatName { get; set; } = null!;

        public int UserId { get; set; }
    }
}
