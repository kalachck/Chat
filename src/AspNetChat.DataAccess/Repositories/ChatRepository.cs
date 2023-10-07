using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;

namespace AspNetChat.DataAccess.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }
    }
}
