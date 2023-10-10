﻿using AspNetChat.DataAccess.Entities;

namespace AspNetChat.DataAccess.Repositories.Abstract
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<List<Chat>> GetByUserIdAsync(int userId);
    }
}
