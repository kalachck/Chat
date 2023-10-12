using AspNetChat.Api.IntegrationTests.Abstract;
using AspNetChat.Api.IntegrationTests.Constants;
using AspNetChat.Models.Message;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;

namespace AspNetChat.Api.IntegrationTests.HubTests
{
    public class ChatHubTests : BaseIntegrationTests
    {
        private readonly HubConnection _hubConnection;

        public ChatHubTests()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(HubConstants.ChatHubUrl, options =>
                {
                    options.HttpMessageHandlerFactory = _ => _factory.Server.CreateHandler();
                })
                .Build();
        }

        [Fact]
        public async Task JoinChatAsync_WhenChatExists_ReturnsMessageHistory()
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            var messages = await _dataManager.CreateMessagesAsync(chat, 3);

            //Act
            await _hubConnection.StartAsync();

            var messageDtos = await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, chat.ChatName);

            //Assert
            messageDtos.Should().NotBeNull().And.HaveCount(messages.Count);
        }

        [Fact]
        public async Task JoinChatAsync_WhenChatDoesNotExist_ReturnsEmptyMessageHistory()
        {
            //Arrange
            //Act
            await _hubConnection.StartAsync();

            var messageDtos = await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, "testChat");

            //Assert
            messageDtos.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact]
        public async Task LeaveChatAsync_WhenRequestIsValid_ReturnsSuccessMessage()
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            var errorMessage = "";
            var receivedMessageEvent = new ManualResetEvent(false);

            _hubConnection.Closed += async (error) =>
            {
                errorMessage = error.Message;
                receivedMessageEvent.Set();
            };

            //Act
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.LeaveChatAsync, chat.ChatName);

            receivedMessageEvent.WaitOne(TimeSpan.FromMilliseconds(10)).Should().BeFalse();
        }

        [Fact]
        public async Task SendMessageAsync_WhenRequestIsValid_ReturnsMessageDto()
        {
            // Arrange
            var chat = await _dataManager.CreateChatAsync();
            var requestModel = _fixture.Build<CreateMessageRequestModel>()
                .With(x => x.ChatName, chat.ChatName)
                .With(x => x.UserId, chat.CreatorId)
                .Create();

            var receivedMessage = "";
            var receivedMessageEvent = new ManualResetEvent(false);

            _hubConnection.On<string>("SendMessage", message =>
            {
                receivedMessage = message;
                receivedMessageEvent.Set();
            });

            // Act
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, "testChat");

            await _hubConnection.InvokeAsync<MessageDto>(HubConstants.SendMessageAsync, requestModel);

            // Assert
            if (receivedMessageEvent.WaitOne(TimeSpan.FromMilliseconds(10)))
            {
                receivedMessage.Should().Be(requestModel.Content);
            }
        }

        [Fact]
        public async Task UpdateMessageAsync_WhenRequestIsValid_ReturnsMessageDto()
        {
            // Arrange
            var chat = await _dataManager.CreateChatAsync();
            var message = await _dataManager.CreateMessageAsync(chat);

            var requestModel = _fixture.Build<UpdateMessageRequestModel>()
                .Create();

            var receivedMessageEvent = new ManualResetEvent(false);

            _hubConnection.On<string>("UpdateMessage", msg =>
            {
                receivedMessageEvent.Set();
            });

            // Act
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, "testChat");

            await _hubConnection.InvokeAsync<MessageDto>(HubConstants.UpdateMessageAsync, message.Id, requestModel);

            // Assert
            if (receivedMessageEvent.WaitOne(TimeSpan.FromMilliseconds(10)))
            {
                message.Should().NotBe(requestModel.Content);
            }
        }

        [Fact]
        public async Task UpdateMessageAsync_WhenMessageDoesNotExist_ReturnsMessageDto()
        {
            // Arrange
            var chat = await _dataManager.CreateChatAsync();

            var requestModel = _fixture.Build<UpdateMessageRequestModel>()
                .Create();

            var receivedMessage = "";
            var receivedMessageEvent = new ManualResetEvent(false);

            _hubConnection.On<string>("UpdateMessage", msg =>
            {
                receivedMessage = msg;

                receivedMessageEvent.Set();
            });

            // Act
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, "testChat");

            await _hubConnection.InvokeAsync<MessageDto>(HubConstants.UpdateMessageAsync, 1, requestModel);

            // Assert
            receivedMessageEvent.WaitOne(TimeSpan.FromMilliseconds(10)).Should().BeFalse();
        }

        [Fact]
        public async Task DeleteMessageAsync_WhenRequestIsValid_ReturnsMessageDto()
        {
            // Arrange
            var chat = await _dataManager.CreateChatAsync();
            var message = await _dataManager.CreateMessageAsync(chat);

            var receivedResult = false;
            var receivedMessageEvent = new ManualResetEvent(false);

            _hubConnection.On<bool>("DeleteMessage", result =>
            {
                receivedResult = result;
                receivedMessageEvent.Set();
            });

            // Act
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, "testChat");

            await _hubConnection.InvokeAsync<bool>(HubConstants.DeleteMessageAsync, message.Id, chat.ChatName);

            // Assert
            if (receivedMessageEvent.WaitOne(TimeSpan.FromMilliseconds(10)))
            {
                receivedResult.Should().BeTrue();
            }
        }

        [Fact]
        public async Task DeleteMessageAsync_WhenMessageDoesNotExists_ReturnsMessageDto()
        {
            // Arrange
            var chat = await _dataManager.CreateChatAsync();
            var message = await _dataManager.CreateMessageAsync(chat);

            var receivedMessageEvent = new ManualResetEvent(false);

            _hubConnection.On<bool>("DeleteMessage", result =>
            {
                receivedMessageEvent.Set();
            });

            // Act
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<List<MessageDto>>(HubConstants.JoinChatAsync, "testChat");

            await _hubConnection.InvokeAsync<bool>(HubConstants.DeleteMessageAsync, message.Id, chat.ChatName);

            // Assert
            receivedMessageEvent.WaitOne(TimeSpan.FromMilliseconds(10)).Should().BeFalse();
        }
    }
}
