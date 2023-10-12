using System.Linq.Expressions;
using AspNetChat.Business.Exceptions;
using AspNetChat.Business.Services;
using AspNetChat.Business.UnitTests.AutoFixtureConfigurations;
using AspNetChat.Business.UnitTests.ServiceTests.Abstract;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.Models.User;
using FluentAssertions;
using Moq;

namespace AspNetChat.Business.UnitTests.ServiceTests
{
    public class UserServiceTests : BaseServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenUserExists_ReturnUserDto(
            int id,
            User user,
            UserDto userDto)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            _mockMapper.Setup(x => x.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetAsync(id);

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserDto>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenUserDoesNotExist_ThrowsNotFoundException(int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value: null);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task CreateAsync_WhenUserDoesNotExist_ReturnUserDto(
            CreateUserRequestModel requestModel,
            User user,
            UserDto userDto)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value: null);

            _mockMapper.Setup(x => x.Map<User>(It.IsAny<CreateUserRequestModel>())).Returns(user);
            _mockMapper.Setup(x => x.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.CreateAsync(requestModel);

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserDto>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _mockUserRepository.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task CreateAsync_WhenUserExists_ThrowsAlreadyExistsException(
            User user,
            CreateUserRequestModel requestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.CreateAsync(requestModel);

            //Assert
            await act.Should().ThrowAsync<AlreadyExistsException>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task UpdateAsync_WhenUserExists_ReturnsUserDto(
            int id,
            User user,
            UserDto userDto,
            UpdateUserRequestModel requestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()));

            _mockMapper.Setup(x => x.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.UpdateAsync(id, requestModel);

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserDto>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task UpdateAsync_WhenUserDoesNotExist_ThrowsNotFoundException(
            int id,
            UpdateUserRequestModel requestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value: null);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.UpdateAsync(id, requestModel);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenUserExists_ReturnsTrue(
            int id,
            User user)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()));

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _mockUserRepository.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenUserDoesNotExist_ThrowsNotFoundException(
            int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value: null);

            var userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
    }
}
