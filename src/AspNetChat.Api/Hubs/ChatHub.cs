using AspNetChat.Business.Services.Abstract;
using AspNetChat.Models.Message;
using Microsoft.AspNetCore.SignalR;

namespace AspNetChat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<List<MessageDto>> JoinChatAsync(string chatName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);

            var messages = await _messageService.GetByChatNameAsync(chatName);

            return messages;
        }

        public async Task LeaveChatAsync(string chatName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
        }

        public async Task SendMessageAsync(CreateMessageRequestModel requestModel)
        {
            var message = await _messageService.CreateAsync(requestModel);

            await Clients.Group(requestModel.ChatName)
                .SendAsync("SendMessage", message.Content);
        }

        public async Task<MessageDto> UpdateMessageAsync(int messageId, UpdateMessageRequestModel requestModel)
        {
            var message = await _messageService.UpdateAsync(messageId, requestModel);

            await Clients.Group(message.ChatName)
                .SendAsync("UpdateMessage", message);

            return message;
        }

        public async Task<bool> DeleteMessageAsync(int messageId, string chatName)
        {
            var result = await _messageService.DeleteAsync(messageId);

            await Clients.Group(chatName)
                .SendAsync("UpdateMessage", result.ToString());

            return result;
        }
    }
}
