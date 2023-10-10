using AspNetChat.Business.Services.Abstract;
using AspNetChat.Models.Chat;
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

        public async Task<List<MessageDto>> JoinChatAsync(int chatId, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

            var messages = await _messageService.GetByChatIdAsync(chatId);

            return messages;
        }

        public async Task LeaveChatAsync(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage(CreateMessageRequestModel requestModel)
        {
            var message = await _messageService.CreateAsync(requestModel);

            await Clients.GroupExcept(requestModel.ChatId.ToString(), new [] { Context.ConnectionId })
                .SendAsync("sendMessage", message.Content);
        }

        public Task UpdateMessage(UpdateMessageRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }
    }
}
