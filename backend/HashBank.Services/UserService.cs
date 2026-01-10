using HashBank.Domain.Entities;
using HashBank.Infrastructure.Repositories;

namespace HashBank.Services
{
    public class UserService(IUserRepository _userRepo, IHashService _hashServ)
    {
        public IUserRepository _userRepository = _userRepo;
        public IHashService _hashService = _hashServ;

        public void Add(User user)
        {
            user.PasswordHash = _hashService.HashPassword(user.PasswordHash);
            _userRepository.Add(user);
        }
    }
}
