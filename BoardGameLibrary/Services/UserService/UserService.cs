using AutoMapper;
using DAL.Models;
using DAL.Models.DTO;
using DAL.Repositories;
using BoardGameLibrary.Helpers.JwtUtils;
using BCryptNet = BCrypt.Net.BCrypt;
namespace BoardGameLibrary.Services.UserService
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public IJwtUtils _jwtUtilis;
        public IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtilis, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtUtilis = jwtUtilis;
            _mapper = mapper;
        }

        public UserAuthResponseDto Authenticate(UserAuthRequestDto model)
        {
            var user = _userRepository.FindByEmail(model.Email);
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                throw new Exception();
            }

            var jwtToken = _jwtUtilis.GenerateJwtToken(user);
            return new UserAuthResponseDto(user, jwtToken);
        }

        public async Task Create(UserAuthRequestDto user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = BCryptNet.HashPassword(user.Password);
            newUser.Role = Role.User;

            await _userRepository.CreateAsync(newUser);
            await _userRepository.SaveAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public User GetById(Guid id)
        {
            return _userRepository.FindById(id);
        }

        public async Task<int> UserRole(Guid id)
        {
            return _userRepository.UserRole(id);
        }

    }
}