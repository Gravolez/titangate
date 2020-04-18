using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _userRepository.FindByLogin(username);
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _userRepository.FindByLogin(username);
            if (user == null)
            {
                return null;
            }

            if (user.PasswordHash != _cryptoService.HashPassword(password, user.Salt))
            {
                return null;
            }

            return user;
        }
    }
}
