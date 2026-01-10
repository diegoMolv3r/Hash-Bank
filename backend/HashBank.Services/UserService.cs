using HashBank.Domain.Entities;
using HashBank.Infrastructure.Repositories;
using HashBank.Services.DTOs;

namespace HashBank.Services
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(CreateUserDto createUserDto);
    }
    public class UserService(IUserRepository _userRepo, IHashService _hashServ, IUnitOfWork _unitOfWork) : IUserService
    {
        public IUserRepository _userRepository = _userRepo;
        public IHashService _hashService = _hashServ;
        public IUnitOfWork _unitOfWork = _unitOfWork;

        public async Task<int> CreateUserAsync(CreateUserDto createUserDto)
        {
            bool emailExists = await _userRepository.UserExistsAsync(createUserDto.Email);

            if (emailExists)
            {
                throw new InvalidOperationException("Email already in use.");
            }
            
            User user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                PasswordHash = _hashService.HashPassword(createUserDto.PasswordHash)
                //PasswordHash = createUserDto.PasswordHash
                // BCrypt.Net.BCrypt.HashPassword(createUserDto.PasswordHash)
            };

            _userRepository.Add(user);
            _unitOfWork.SaveChanges();

            return user.Id;
        }
    }
}
