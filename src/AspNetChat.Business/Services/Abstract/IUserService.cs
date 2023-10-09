using AspNetChat.Models.User;

namespace AspNetChat.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(int id);

        Task<UserDto> CreateAsync(CreateUserRequestModel requestModel);

        Task<UserDto> UpdateAsync(int id, UpdateUserRequestModel requestModel);

        Task<bool> DeleteAsync(int id);
    }
}
