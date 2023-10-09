namespace AspNetChat.Models.User
{
    public class CreateUserRequestModel
    {
        public string UserName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Surname { get; set; }
    }
}
