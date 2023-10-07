using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;

namespace AspNetChat.DataAccess.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }
    }
}
