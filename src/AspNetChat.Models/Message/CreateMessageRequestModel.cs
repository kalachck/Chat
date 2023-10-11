namespace AspNetChat.Models.Message
{
    public class CreateMessageRequestModel
    {
        public string Content { get; set; }

        public int UserId { get; set; }

        public string ChatName { get; set; }
    }
}
