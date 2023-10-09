﻿namespace AspNetChat.Models.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Surname { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
