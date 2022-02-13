using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using AutoMapper;
using Domain.DTOs.User;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;

        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            UserEntity userEntity = await _repository.GetAsync(id);

            return _mapper.Map<UserDto>(userEntity) ?? new UserDto();
        }

        public async Task<IList<UserDto>> GetAll()
        {
            var list = await _repository.GetAllAsync();

            return _mapper.Map<List<UserDto>>(list);
        }

        public async Task<UserCreateResultDTO> Post(UserDtoCreate user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            userEntity = await _repository.InsertAsync(userEntity);

            return _mapper.Map<UserCreateResultDTO>(userEntity);
        }

        public async Task<UserUpdateResultDTO> Put(UserDtoUpdate user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            userEntity = await _repository.UpdateAsync(userEntity);

            return _mapper.Map<UserUpdateResultDTO>(userEntity);
        }
    }
}
