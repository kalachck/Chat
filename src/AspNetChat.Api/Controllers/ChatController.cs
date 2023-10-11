using AspNetChat.Business.Services.Abstract;
using AspNetChat.Models.Chat;
using Microsoft.AspNetCore.Mvc;

namespace AspNetChat.Api.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
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

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, UpdateChatRequestModel requestModel)
        {
            var result = await _chatService.UpdateAsync(id, requestModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string chatName, int userId)
        {
            var result = await _chatService.DeleteAsync(chatName, userId);

            return Ok(result);
        }
    }
}
