using AspNetChat.DataAccess.Entities;
using AutoFixture;

namespace AspNetChat.Api.IntegrationTests.AutoFixtureConfigurations
{
    public class AutoFixtureCustomizations : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<User>(cfg =>
                cfg.Without(x => x.Id)
                    .Without(x => x.Messages)
                    .Without(x => x.Chats));

            fixture.Customize<Chat>(cfg =>
                cfg.Without(x => x.Id)
                    .Without(x => x.Messages)
                    .Without(x => x.User));

            fixture.Customize<Message>(cfg => 
                cfg.Without(x => x.Id)
                    .Without(x => x.Chat)
                    .Without(x => x.User));
        }
    }
}
