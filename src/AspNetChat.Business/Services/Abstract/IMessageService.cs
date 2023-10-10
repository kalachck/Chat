using AspNetChat.Models.Message;

namespace AspNetChat.Business.Services.Abstract
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetByChatNameAsync(string chatName);

        Task<MessageDto> CreateAsync(CreateMessageRequestModel requestModel);

        Task<MessageDto> UpdateAsync(int id, UpdateMessageRequestModel requestModel);

        Task<bool> DeleteAsync(int id);
    }
}
