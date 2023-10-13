using AspNetChat.Business.Exceptions;
using AspNetChat.Business.Services;
using AspNetChat.Business.UnitTests.AutoFixtureConfigurations;
using AspNetChat.Business.UnitTests.ServiceTests.Abstract;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.Models.Chat;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace AspNetChat.Business.UnitTests.ServiceTests
{
    public class ChatServiceTests : BaseServiceTests
    {
        private readonly Mock<IChatRepository> _mockChatRepository;

        public ChatServiceTests()
        {
            _mockChatRepository = new Mock<IChatRepository>();
        }


        [Theory]
        [CustomAutoData]
        public async Task GetChatsAsync_WhenRequestIsValid_ReturnListOfChatDto(
            int page,
            int take,
            List<Chat> chats,
            List<ChatDto> chatDtos)
        {
            //Arrange
            _mockChatRepository.Setup(x => x.GetChatsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(chats);

            _mockMapper.Setup(x => x.Map<List<ChatDto>>(It.IsAny<List<Chat>>()))
                .Returns(chatDtos);

            var chatService = new ChatService(_mockChatRepository.Object, _mockMapper.Object);

            //Act
            var result = await chatService.GetChatsAsync(page, take);

            //Assert
            result.Should().NotBeNull().And.AllBeOfType<ChatDto>();

            _mockChatRepository.Verify(x => x.GetChatsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetByUserIdAsync_WhenRequestIsValid_ReturnListOfChatDto(
            int userId,
            List<Chat> chats,
            List<ChatDto> chatDtos)
        {
            //Arrange
            _mockChatRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(chats);

            _mockMapper.Setup(x => x.Map<List<ChatDto>>(It.IsAny<List<Chat>>()))
                .Returns(chatDtos);

            var chatService = new ChatService(_mockChatRepository.Object, _mockMapper.Object);

            //Act
            var result = await chatService.GetByUserIdAsync(userId);

            //Assert
            result.Should().NotBeNull().And.AllBeOfType<ChatDto>();

            _mockChatRepository.Verify(x => x.GetByUserIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task CreateAsync_WhenUserDoesNotExist_ReturnUserDto(
            CreateChatRequestModel requestModel,
            Chat chat,
            ChatDto chatDto)
        {
            //Arrange
            _mockChatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()))
                .ReturnsAsync(value: null);

            _mockMapper.Setup(x => x.Map<Chat>(It.IsAny<CreateChatRequestModel>())).Returns(chat);
            _mockMapper.Setup(x => x.Map<ChatDto>(It.IsAny<Chat>())).Returns(chatDto);

            var chatService = new ChatService(_mockChatRepository.Object, _mockMapper.Object);

            //Act
            var result = await chatService.CreateAsync(requestModel);

            //Assert
            result.Should().NotBeNull().And.BeOfType<ChatDto>();

            _mockChatRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()), Times.Once);
            _mockChatRepository.Verify(x => x.CreateAsync(It.IsAny<Chat>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task CreateAsync_WhenUserExists_ThrowsAlreadyExistsException(
            Chat chat,
            CreateChatRequestModel requestModel)
        {
            //Arrange
            _mockChatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()))
                .ReturnsAsync(chat);

            var chatService = new ChatService(_mockChatRepository.Object, _mockMapper.Object);

            //Act
            var act = () => chatService.CreateAsync(requestModel);

            //Assert
            await act.Should().ThrowAsync<AlreadyExistsException>();

            _mockChatRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenUserExists_ReturnsTrue(
            int userId,
            string chatName,
            Chat chat)
        {
            //Arrange
            _mockChatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()))
                .ReturnsAsync(chat);

            _mockChatRepository.Setup(x => x.DeleteAsync(It.IsAny<Chat>()));

            var chatService = new ChatService(_mockChatRepository.Object, _mockMapper.Object);

            //Act
            var result = await chatService.DeleteAsync(chatName, userId);

            //Assert
            result.Should().BeTrue();

            _mockChatRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()), Times.Once);
            _mockChatRepository.Verify(x => x.DeleteAsync(It.IsAny<Chat>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenUserDoesNotExist_ReturnsFalse(
            int userId,
            string chatName)
        {
            //Arrange
            _mockChatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()))
                .ReturnsAsync(value: null);

            var chatService = new ChatService(_mockChatRepository.Object, _mockMapper.Object);

            //Act
            var result = await chatService.DeleteAsync(chatName, userId);

            //Assert
            result.Should().BeFalse();

            _mockChatRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>()), Times.Once);
        }
    }
}
