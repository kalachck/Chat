using AspNetChat.Models.Message;

namespace AspNetChat.Business.Services.Abstract
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetByChatIdAsync(int chatId);

        Task<MessageDto> CreateAsync(CreateMessageRequestModel requestModel);

        Task<MessageDto> UpdateMessage(int id, UpdateMessageRequestModel requestModel);

        Task<bool> DeleteMessage(int id);
    }
}
