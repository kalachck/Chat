using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AspNetChat.DataAccess.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<List<Message>> GetByChatIdAsync(int chatId)
        {
            return await _databaseContext.Messages.Where(x => x.ChatId == chatId).ToListAsync();
        }
    }
}
