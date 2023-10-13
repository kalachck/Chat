using AspNetChat.Api.Hubs;
using AspNetChat.Business.Services.Abstract;
using AspNetChat.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AspNetChat.Api.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatService chatService, 
            IHubContext<ChatHub> hubContext)
        {
            _chatService = chatService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetChatsAsync(int page, int take)
        {
            var result = await _chatService.GetChatsAsync(page, take);

            return Ok(result);
        }

        [HttpGet]
        [Route("user")]
        public async Task<ActionResult> GetByUserIdAsync(int userId)
        {
            var result = await _chatService.GetByUserIdAsync(userId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateChatRequestModel requestModel)
        {
            var result = await _chatService.CreateAsync(requestModel);

            return Ok(result);
        }
    }
}
