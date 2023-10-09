using AspNetChat.Business.Exceptions;
using AspNetChat.Business.Services.Abstract;
using AspNetChat.DataAccess.Entities;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.Models.User;
using AutoMapper;

namespace AspNetChat.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, 
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserRequestModel requestModel)
        {
            var user = await _userRepository.GetAsync(x => x.UserName == requestModel.UserName);

            if (user != null)
            {
                throw new AlreadyExistsException("User already exists!");
            }

            await _userRepository.CreateAsync(user!);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(int id, UpdateUserRequestModel requestModel)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User not found!");
            }

            user = _mapper.Map<User>(requestModel);
            user.Id = id;

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User not found!");
            }

            await _userRepository.DeleteAsync(user);

            return true;
        }
    }
}
