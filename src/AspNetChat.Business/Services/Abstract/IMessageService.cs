using AspNetChat.Models.Message;

namespace AspNetChat.Business.Services.Abstract
{
    public interface IMessageService
    {
        Task<MessageDto> CreateMessageAsync(CreateMessageRequestModel requestModel);

        Task<MessageDto> UpdateMessageAsync(int id, UpdateMessageRequestModel requestModel);

        Task<bool> DeleteMessageAsync(int id);
    }
}
