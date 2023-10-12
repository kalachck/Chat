using AspNetChat.Business.Exceptions;
using AspNetChat.Business.Services;
using AspNetChat.Business.UnitTests.AutoFixtureConfigurations;
using AspNetChat.Business.UnitTests.ServiceTests.Abstract;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.Models.Message;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace AspNetChat.Business.UnitTests.ServiceTests
{
    public class MessageServiceTests : BaseServiceTests
    {
        private readonly Mock<IMessageRepository> _mockMessageRepository;

        public MessageServiceTests()
        {
            _mockMessageRepository = new Mock<IMessageRepository>();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetByChatNameAsync_WhenRequestIsValid_ReturnsListOfMessages(
            List<Message> messages,
            List<MessageDto> messageDtos,
            string chatName)
        {
            //Arrange
            _mockMessageRepository.Setup(x => x.GetByChatNameAsync(It.IsAny<string>()))
                .ReturnsAsync(messages);

            _mockMapper.Setup(x => x.Map<List<MessageDto>>(It.IsAny<List<Message>>()))
                .Returns(messageDtos);

            var messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);

            //Act
            var result = await messageService.GetByChatNameAsync(chatName);

            //Assert
            result.Should().NotBeNull();

            _mockMessageRepository.Verify(x => x.GetByChatNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task CreateAsync_WhenRequestIsValid_ReturnMessageDto(
            CreateMessageRequestModel requestModel,
            MessageDto messageDto,
            Message message)
        {
            //Arrange
            _mockMessageRepository.Setup(x => x.CreateAsync(It.IsAny<Message>()));

            _mockMapper.Setup(x => x.Map<Message>(It.IsAny<CreateMessageRequestModel>()))
                .Returns(message);

            _mockMapper.Setup(x => x.Map<MessageDto>(It.IsAny<Message>()))
                .Returns(messageDto);

            var messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);

            //Act
            var result = await messageService.CreateAsync(requestModel);

            //Assert
            result.Should().NotBeNull().And.BeOfType<MessageDto>();

            _mockMessageRepository.Verify(x => x.CreateAsync(It.IsAny<Message>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task UpdateAsync_WhenMessageExists_ReturnsMessageDto(
            int id,
            Message message,
            MessageDto messageDto,
            UpdateMessageRequestModel requestModel)
        {
            //Arrange
            _mockMessageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()))
                .ReturnsAsync(message);

            _mockMessageRepository.Setup(x => x.UpdateAsync(It.IsAny<Message>()));

            _mockMapper.Setup(x => x.Map<MessageDto>(It.IsAny<Message>())).Returns(messageDto);

            var messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);

            //Act
            var result = await messageService.UpdateAsync(id, requestModel);

            //Assert
            result.Should().NotBeNull().And.BeOfType<MessageDto>();

            _mockMessageRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()), Times.Once);
            _mockMessageRepository.Verify(x => x.UpdateAsync(It.IsAny<Message>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task UpdateAsync_WhenMessageDoesNotExist_ThrowsNotFoundException(
            int id,
            UpdateMessageRequestModel requestModel)
        {
            //Arrange
            _mockMessageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()))
                .ReturnsAsync(value: null);

            var messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);

            //Act
            var act = () => messageService.UpdateAsync(id, requestModel);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockMessageRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenMessageExists_ReturnsMessageDto(
            int id,
            Message message)
        {
            //Arrange
            _mockMessageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()))
                .ReturnsAsync(message);

            _mockMessageRepository.Setup(x => x.DeleteAsync(It.IsAny<Message>()));

            var messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);

            //Act
            var result = await messageService.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();

            _mockMessageRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()), Times.Once);
            _mockMessageRepository.Verify(x => x.DeleteAsync(It.IsAny<Message>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenMessageDoesNotExist_ThrowsNotFoundException(
            int id)
        {
            //Arrange
            _mockMessageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()))
                .ReturnsAsync(value: null);

            var messageService = new MessageService(_mockMessageRepository.Object, _mockMapper.Object);

            //Act
            var act = () => messageService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockMessageRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()), Times.Once);
        }
    }
}
