using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AspNetChat.DataAccess.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<List<Chat>> GetChatsAsync(int page, int take)
        {
            return await _databaseContext.Chats.Skip((page - 1) * take).Take(take).ToListAsync();
        }

        public async Task<List<Chat>> GetByUserIdAsync(int userId)
        {
            return await _databaseContext.Chats.Where(x => x.CreatorId == userId).ToListAsync();
        }
    }
}
