using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Domain.DTOs;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<object> FindByLogin(LoginDTO loginDto)
        {
            bool isEmailFilled = string.IsNullOrWhiteSpace(loginDto.Email) == false;

            if (loginDto != null && isEmailFilled)
            {
                return await _userRepository.FindByLogin(loginDto.Email);
            }
            else
            {
                return null;
            }
        }
    }
}
