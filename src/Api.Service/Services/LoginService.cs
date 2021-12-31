using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Domain.DTOs;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration;

        public LoginService(IUserRepository userRepository,
                            SigningConfiguration signingConfiguration,
                            TokenConfiguration tokenConfiguration,
                            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email))
            {
                return ErrorObject();
            }

            UserEntity user = await _userRepository.FindByLogin(loginDto.Email);

            if (user == null)
            {
                return ErrorObject();
            }
            else
            {
                var identity = GerarClaimsIdentity(user.Email);

                DateTime createDate = DateTime.Now;
                DateTime expiryDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();

                string token = CreateToken(identity, createDate, expiryDate, handler);

                return SuccessObject(createDate, expiryDate, token, loginDto);
            }
        }

        private ClaimsIdentity GerarClaimsIdentity(string login)
        {
            var identity = new ClaimsIdentity(
                        new GenericIdentity(login),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, login)
                        }
                    );

            return identity;
        }

        private object ErrorObject()
        {
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expiryDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expiryDate
            });

            string token = handler.WriteToken(securityToken);

            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expiryDate, string token, LoginDTO login)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("dd-MM-yyyy HH:mm:ss"),
                expiration = expiryDate.ToString("dd-MM-yyyy HH:mm:ss"),
                acessToken = token,
                userName = login.Email,
                message = "Usuário logado com sucesso!"
            };
        }
    }
}
