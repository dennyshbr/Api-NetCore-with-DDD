using Api.Domain.Entities;
using Api.Service.Test.User;
using Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper : BaseTestService
    {
        private UserFake _userFake;

        public UserMapper()
        {
            _userFake = new UserFake();
        }

        [Fact]
        public void MapperToDtos()
        {
            var userDto = Mapper.Map<UserDto>(_userFake.UserEntity);

            Assert.NotNull(userDto);
            Assert.Equal(_userFake.UserEntity.Id, userDto.Id);

            var userDtoCreate = Mapper.Map<UserDtoCreate>(_userFake.UserEntity);

            Assert.NotNull(userDtoCreate);
            Assert.Equal(_userFake.UserEntity.Name, userDtoCreate.Name);

            var userDtoUpdate = Mapper.Map<UserDtoUpdate>(_userFake.UserEntity);

            Assert.NotNull(userDtoUpdate);
            Assert.Equal(_userFake.UserEntity.Name, userDtoUpdate.Name);

            var userCreateResult = Mapper.Map<UserCreateResultDTO>(_userFake.UserEntity);

            Assert.NotNull(userCreateResult);
            Assert.Equal(_userFake.UserEntity.Id, userCreateResult.Id);

            var userUpdateResult = Mapper.Map<UserUpdateResultDTO>(_userFake.UserEntity);

            Assert.NotNull(userUpdateResult);
            Assert.Equal(_userFake.UserEntity.UpdatedAt, userUpdateResult.UpdatedAt);
        }

        [Fact]
        public void MapperToUserEntity()
        {
            UserEntity userEntity = Mapper.Map<UserEntity>(_userFake.UserDto);

            Assert.NotNull(userEntity);
            Assert.Equal(userEntity.Id, _userFake.UserDto.Id);

            userEntity = Mapper.Map<UserEntity>(_userFake.UserDtoCreate);

            Assert.NotNull(userEntity);
            Assert.Equal(userEntity.Name, _userFake.UserDtoCreate.Name);

            userEntity = Mapper.Map<UserEntity>(_userFake.UserDtoUpdate);

            Assert.NotNull(userEntity);
            Assert.Equal(userEntity.Email, _userFake.UserDtoUpdate.Email);

            userEntity = Mapper.Map<UserEntity>(_userFake.UserCreateResultDTO);

            Assert.NotNull(userEntity);
            Assert.Equal(userEntity.CreatedAt, _userFake.UserCreateResultDTO.CreatedAt);

            userEntity = Mapper.Map<UserEntity>(_userFake.UserUpdateResultDTO);

            Assert.NotNull(userEntity);
            Assert.Equal(userEntity.UpdatedAt, _userFake.UserUpdateResultDTO.UpdatedAt);
        }

        [Fact]
        public void MapperToDtoList()
        {
            var listUserDto = Mapper.Map<IList<UserDto>>(_userFake.ListUserEntity);

            Assert.Equal(listUserDto.Count, _userFake.ListUserEntity.Count);
        }

        [Fact]
        public void MapperToUserEntityList()
        {
            List<UserDto> listDto = new List<UserDto>()
            {
                new UserDto(),
                new UserDto(),
                new UserDto()
            };

            var listUserEntity = Mapper.Map<IList<UserEntity>>(listDto);

            Assert.Equal(listUserEntity.Count, listDto.Count);
        }
    }
}
