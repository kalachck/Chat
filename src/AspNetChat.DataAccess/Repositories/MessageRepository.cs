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

        public async Task<List<Message>> GetByChatNameAsync(string chatName)
        {
            return await _databaseContext.Messages
                .Where(x => x.Chat.ChatName == chatName).ToListAsync();
        }
    }
}
