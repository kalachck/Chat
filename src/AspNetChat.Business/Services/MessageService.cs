﻿using AspNetChat.Business.Exceptions;
using AspNetChat.Business.Services.Abstract;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.Models.Message;
using AutoMapper;

namespace AspNetChat.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> GetByChatNameAsync(string chatName)
        {
            var messages = await _messageRepository.GetByChatNameAsync(chatName);

            return _mapper.Map<List<MessageDto>>(messages);
        }

        public async Task<MessageDto> CreateAsync(CreateMessageRequestModel requestModel)
        {
            var message = _mapper.Map<Message>(requestModel);

            await _messageRepository.CreateAsync(message);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<MessageDto> UpdateAsync(int id, UpdateMessageRequestModel requestModel)
        {
            var message = await _messageRepository.GetAsync(x => x.Id == id);

            if (message == null)
            {
                throw new NotFoundException("Message not found!");
            }

            message.Content = requestModel.Content;

            await _messageRepository.UpdateAsync(message);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _messageRepository.GetAsync(x => x.Id == id);

            if (message == null)
            {
                throw new NotFoundException("Message not found!");
            }

            await _messageRepository.DeleteAsync(message);

            return true;
        }
    }
}
