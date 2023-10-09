using AspNetChat.DataAccess.Entities;
using AspNetChat.Models.Chat;
using AspNetChat.Models.Message;
using AspNetChat.Models.User;
using AutoMapper;

namespace AspNetChat.Business.Mappers
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<CreateMessageRequestModel, Message>();

            CreateMap<User, UserDto>();
            CreateMap<CreateUserRequestModel, User>();

            CreateMap<Chat, ChatDto>().ReverseMap();
            CreateMap<CreateChatRequestModel, Chat>();
        }
    }
}
