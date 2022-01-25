using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.User;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<bool> Delete(Guid id);
        Task<UserDto> Get(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserCreateResultDTO> Post(UserDtoCreate user);
        Task<UserUpdateResultDTO> Put(UserDtoUpdate user);
    }
}
