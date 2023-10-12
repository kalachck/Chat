using AspNetChat.Api.IntegrationTests.AutoFixtureConfigurations;
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


            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize(new AutoFixtureCustomizations());
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
                .Without(x => x.Id)
                .With(x => x.CreatorId, user.Id)
                .Create();

            _databaseContext.Chats.Add(chat);
            await _databaseContext.SaveChangesAsync();

            return chat;
        }

        public async Task<Message> CreateMessageAsync(Chat chat)
        {
            var message = _fixture.Build<Message>()
                .Without(x => x.Id)
                .With(x => x.ChatName, chat.ChatName)
                .With(x => x.UserId, chat.CreatorId)
                .Create();

            _databaseContext.Messages.Add(message);
            await _databaseContext.SaveChangesAsync();

            return message;
        }

        public async Task<List<Message>> CreateMessagesAsync(Chat chat, int numberOfMessages)
        {
            var messages = _fixture.Build<Message>()
                .Without(x => x.Id)
                .With(x => x.ChatName, chat.ChatName)
                .With(x => x.UserId, chat.CreatorId)
                .CreateMany(numberOfMessages).ToList();

            _databaseContext.Messages.AddRange(messages);
            await _databaseContext.SaveChangesAsync();

            return messages;
        }
    }
}
