using AspNetChat.Api.IntegrationTests.Constants;
using AspNetChat.Api.IntegrationTests.ControllerTests.Abstract;
using AspNetChat.Models.Chat;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using System.Net;

namespace AspNetChat.Api.IntegrationTests.ControllerTests
{
    public class ChatControllerTests : BaseControllerTests
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

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenEntityExists_ReturnsOk(
            UpdateChatRequestModel requestModel)
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var (chatDto, response) = await ExecuteWithFullResponseAsync<ChatDto>(
                $"{ApiConstants.ChatApi}?id={chat.Id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            chatDto.ChatName.Should().NotBe(chat.ChatName);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExistsWithChatName_ReturnsConflict()
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            var requestModel = _fixture.Build<UpdateChatRequestModel>()
                .With(x => x.ChatName, chat.ChatName)
                .Create();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.ChatApi}?id={chat.Id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound(
            UpdateChatRequestModel requestModel)
        {
            //Arrange
            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.ChatApi}?id={0}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var requestModel = _fixture.Build<UpdateChatRequestModel>()
                .With(x => x.ChatName, string.Empty).Create();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.ChatApi}?id={id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.ChatApi}?chatName={chat.ChatName}&userId={chat.CreatorId}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenAccessDenied_ReturnsForbidden()
        {
            //Arrange
            var chat = await _dataManager.CreateChatAsync();

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.ChatApi}?chatName={chat.ChatName}&userId={0}", HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.ChatApi}?chatName={string.Empty}&userId={string.Empty}",
                HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
