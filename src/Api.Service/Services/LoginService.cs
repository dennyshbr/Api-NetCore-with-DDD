using System;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<object> FindByLogin(UserEntity user)
        {
            bool isEmailFilled = string.IsNullOrWhiteSpace(user.Email) == false;

            if (user != null && isEmailFilled)
            {
                return await _userRepository.FindByLogin(user.Email);
            }
            else
            {
                return null;
            }
        }
    }
}
