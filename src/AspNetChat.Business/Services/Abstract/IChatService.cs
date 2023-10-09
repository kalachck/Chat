using AspNetChat.Models.Chat;

namespace AspNetChat.Business.Services.Abstract
{
    public interface IChatService
    {
        Task<ChatDto> GetAsync(int id);

        Task<ChatDto> CreateAsync(CreateChatRequestModel requestModel);

        Task<ChatDto> UpdateAsync(int id, UpdateChatRequestModel requestModel);

        Task<bool> DeleteAsync(int chatId, int userId);
    }
}
