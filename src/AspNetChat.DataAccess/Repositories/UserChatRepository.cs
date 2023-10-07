using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;

namespace AspNetChat.DataAccess.Repositories
{
    public class UserChatRepository : BaseRepository<UserChat>, IUserChatRepository
    {
        public UserChatRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }
    }
}
