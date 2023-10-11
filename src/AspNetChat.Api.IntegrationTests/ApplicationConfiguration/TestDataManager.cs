using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities;
using AutoFixture;

namespace AspNetChat.Api.IntegrationTests.ApplicationConfiguration
{
    public class TestDataManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public TestDataManager(DatabaseContext databaseContext, Fixture fixture)
        {
            _databaseContext = databaseContext;
            _fixture = fixture;
        }

        public async Task<User> CreateUserAsync()
        {
            var user = _fixture.Create<User>();

            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();

            return user;
        }

        public async Task<Chat> CreateChatAsync()
        {
            var user = await CreateUserAsync();

            var chat = _fixture.Build<Chat>()
                .With(x => x.CreatorId, user.Id)
                .Create();

            _databaseContext.Chats.Add(chat);
            await _databaseContext.SaveChangesAsync();

            return chat;
        }

        public async Task<Message> CreateMessageByChatCreatorAsync()
        {
            var chat = await CreateChatAsync();

            var message = _fixture.Build<Message>()
                .With(x => x.ChatName, chat.ChatName)
                .With(x => x.UserId, chat.CreatorId)
                .Create();

            _databaseContext.Messages.Add(message);
            await _databaseContext.SaveChangesAsync();

            return message;
        }

        public async Task<Message> CreateMessageByChatUserAsync()
        {
            var user = await CreateUserAsync();

            var chat = await CreateChatAsync();

            var message = _fixture.Build<Message>()
                .With(x => x.ChatName, chat.ChatName)
                .With(x => x.UserId, user.Id)
                .Create();

            _databaseContext.Messages.Add(message);
            await _databaseContext.SaveChangesAsync();

            return message;
        }
    }
}
