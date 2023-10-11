using AspNetChat.DataAccess.Repositories.Abstract;
using AutoFixture;
using Moq;

namespace AspNetChat.Business.UnitTests.ServiceTests
{
    public class ChatServiceTests
    {
        private readonly Mock<IChatRepository> _mockChatRepository;
        private readonly Fixture _fixture;

        public ChatServiceTests()
        {
            _mockChatRepository = new Mock<IChatRepository>();
            _fixture = new Fixture();
        }
    }

    //[Fact]
    //public async Task GetChatsAsync_WhenChatsExist_ReturnsListOfChats()
    //{
    //    //Arrange
    //    //Act
    //    //Assert
    //}
}
