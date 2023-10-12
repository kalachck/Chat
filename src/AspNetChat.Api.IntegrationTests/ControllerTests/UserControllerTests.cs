using AspNetChat.Api.IntegrationTests.Constants;
using AspNetChat.Api.IntegrationTests.ControllerTests.Abstract;
using System.Net;
using AspNetChat.Models.User;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Xunit2;

namespace AspNetChat.Api.IntegrationTests.ControllerTests
{
    public class UserControllerTests : BaseControllerTests
    {
        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var (userDto, response) =
                await ExecuteWithFullResponseAsync<UserDto>($"{ApiConstants.UserApi}?id={user.Id}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}?id={-1}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}?id={string.Empty}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoData]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk(
            CreateUserRequestModel requestModel)
        {
            //Arrange
            var requestBody = BuildRequestBody(requestModel);

            //Act
            var (userDto, response) = await ExecuteWithFullResponseAsync<UserDto>(
                ApiConstants.UserApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.UserName.Should().Be(requestModel.UserName);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Build<CreateUserRequestModel>()
                .With(x => x.UserName, string.Empty).Create();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.UserApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenEntityExists_ReturnsOk(
            UpdateUserRequestModel requestModel)
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var (userDto, response) = await ExecuteWithFullResponseAsync<UserDto>(
                $"{ApiConstants.UserApi}?id={user.Id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.UserName.Should().Be(user.UserName);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound(
            UpdateUserRequestModel requestModel)
        {
            //Arrange
            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}?id={0}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var requestModel = _fixture.Build<UpdateUserRequestModel>()
                .With(x => x.Name, string.Empty).Create();

            var requestBody = BuildRequestBody(requestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}?id={id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={user.Id}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={0}", HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={string.Empty}",
                HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
