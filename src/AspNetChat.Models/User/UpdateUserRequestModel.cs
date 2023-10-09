namespace AspNetChat.Models.User
{
    public class UpdateUserRequestModel
    {
        public string Name { get; set; } = null!;

        public string? Surname { get; set; }
    }
}
