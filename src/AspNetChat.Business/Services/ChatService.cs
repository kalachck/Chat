using AspNetChat.Business.Exceptions;
using AspNetChat.Business.Services.Abstract;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.Models.Chat;
using AutoMapper;

namespace AspNetChat.Business.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository,
            IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<List<ChatDto>> GetChatsAsync(int page, int take)
        {
            var chats = await _chatRepository.GetChatsAsync(page, take);

            return _mapper.Map<List<ChatDto>>(chats);
        }

        public async Task<List<ChatDto>> GetByUserIdAsync(int userId)
        {
            var chats = await _chatRepository.GetByUserIdAsync(userId);

            return _mapper.Map<List<ChatDto>>(chats);
        }

        public async Task<ChatDto> CreateAsync(CreateChatRequestModel requestModel)
        {
            var chat = await _chatRepository.GetAsync(x => x.ChatName == requestModel.ChatName);

            if (chat == null)
            {
                throw new AlreadyExistsException("This chat already exists");
            }

            chat = _mapper.Map<Chat>(requestModel);

            await _chatRepository.CreateAsync(chat);

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ChatDto> UpdateAsync(int id, UpdateChatRequestModel requestModel)
        {
            var chat = await _chatRepository.GetAsync(x => x.Id == id);

            if (chat == null)
            {
                throw new NotFoundException("Chat not found");
            }

            chat.ChatName = requestModel.ChatName;

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<bool> DeleteAsync(int chatId, int userId)
        {
            var chat = await _chatRepository.GetAsync(x => x.CreatorId == userId);

            if (chat == null)
            {
                throw new DeniedAccessException("No permission to delete this chat");
            }

            await _chatRepository.DeleteAsync(chat);

            return true;
        }
    }
}
