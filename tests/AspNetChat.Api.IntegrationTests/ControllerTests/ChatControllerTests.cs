using AspNetChat.Api.IntegrationTests.Abstract;
using AspNetChat.Api.IntegrationTests.Constants;
using AspNetChat.Models.Chat;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using System.Net;

namespace AspNetChat.Api.IntegrationTests.ControllerTests
{
    public class ChatControllerTests : BaseIntegrationTests
    {
        [Theory]
        [AutoData]
        public async Task GetChatsAsync_WhenRequestIsValid_ReturnsOk(
            int page,
            int take)
        {
            //Arrange
            //Act
            var (chatDtos, response) = await ExecuteWithFullResponseAsync<List<ChatDto>>(
                    $"{ApiConstants.ChatApi}?page={page}&take={take}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            chatDtos!.Should().NotBeNull().And.BeAssignableTo<IEnumerable<ChatDto>>();
        }

        [Fact]
        public async Task GetChatsAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.ChatApi}?page={string.Empty}&take={string.Empty}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoData]
        public async Task GetByUserIdAsync_WhenRequestIsValid_ReturnsOk(
            int userId)
        {
            //Arrange
            //Act
            var (chatDtos, response) = await ExecuteWithFullResponseAsync<List<ChatDto>>(
                    $"{ApiConstants.ChatApi}/user?userId={userId}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            chatDtos!.Should().NotBeNull().And.BeAssignableTo<IEnumerable<ChatDto>>();
        }

        [Fact]
        public async Task GetByUserIdAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.ChatApi}/user?userId={string.Empty}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoData]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk(
            CreateChatRequestModel requestModel)
        {
            //Arrange
            var requestBody = BuildRequestBody(requestModel);

            //Act
            var (chatDto, response) = await ExecuteWithFullResponseAsync<ChatDto>(
                ApiConstants.ChatApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            chatDto.ChatName.Should().Be(requestModel.ChatName);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExists_ReturnsConflict()
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            var requestModel = _fixture.Build<CreateChatRequestModel>()
                .With(x => x.ChatName, chat.ChatName)
                .Create();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.ChatApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Build<CreateChatRequestModel>()
                .With(x => x.ChatName, string.Empty).Create();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.ChatApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
