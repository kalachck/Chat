using AspNetChat.Models.Chat;

namespace AspNetChat.Business.Services.Abstract
{
    public interface IChatService
    {
        Task<List<ChatDto>> GetChatsAsync(int page, int take);

        Task<List<ChatDto>> GetByUserIdAsync(int userId);

        Task<ChatDto> CreateAsync(CreateChatRequestModel requestModel);

        Task<ChatDto> UpdateAsync(int id, UpdateChatRequestModel requestModel);

        Task<bool> DeleteAsync(int chatId, int userId);
    }
}
