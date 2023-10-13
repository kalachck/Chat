using AspNetChat.Business.Services.Abstract;
using AspNetChat.Models.Message;
using Microsoft.AspNetCore.SignalR;

namespace AspNetChat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;

        public ChatHub(IMessageService messageService, 
            IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
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

            await Clients.GroupExcept(requestModel.ChatName, new []{ Context.ConnectionId })
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
                .SendAsync("DeleteMessage", result.ToString());

            return result;
        }

        public async Task DeleteChat(string chatName, int userId)
        {
            var result = await _chatService.DeleteAsync(chatName, userId);

            if (result)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);

                Context.Abort();

                await Clients.Groups(chatName).SendAsync("Disconnect");
            }
            else
            {
                await Clients.Caller
                    .SendAsync("PermissionDenied", "You don't have permission to delete this chat.");
            }
        }
    }
}
