using AspNetChat.DataAccess.Entities;

namespace AspNetChat.DataAccess.Repositories.Abstract
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetByChatIdAsync(int chatId);
    }
}
