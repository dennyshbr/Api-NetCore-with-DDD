using Domain.DTOs.User;
using System;
using System.Linq;
using System.Collections.Generic;
using Faker;
using Api.Domain.Entities;

namespace Api.Service.Test.User
{
    public class UserFake
    {
        public string UserEmail { get; private set; }
        public string UserEmailUpdate { get; private set; }
        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string UserNameUpdate { get; private set; }

        public IList<UserEntity> ListUserEntity { get; private set; }
        public UserDto UserDto { get; private set; }

        public UserDtoCreate UserDtoCreate { get; private set; }

        public UserCreateResultDTO UserCreateResultDTO { get; private set; }

        public UserDtoUpdate UserDtoUpdate { get; private set; }

        public UserUpdateResultDTO UserUpdateResultDTO { get; private set; }

        public UserEntity UserEntity { get; private set; }

        public UserFake()
        {
            UserEmail = Internet.Email();
            UserEmailUpdate = Internet.Email();
            UserName = Name.FullName();
            UserNameUpdate = Name.FullName();
            UserId = Guid.NewGuid();

            FillUserList();
            CreateUserDTO();
            CreateUserDtoCreate();
            CreateUserCreateResultDTO();
            CreateUserDtoUpdate();
            CreateUserUpdateResultDTO();
            CreateUserEntity();
        }

        public void FillUserList()
        {
            ListUserEntity = new List<UserEntity>();

            for (int i = 0; i < 10; i++)
            {
                var userEntity = new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Name.FullName(),
                    Email = Internet.Email(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now.AddDays(2)
                };

                ListUserEntity.Add(userEntity);
            }
        }

        public void CreateUserDTO()
        {
            UserDto = new UserDto()
            {
                CreatedAt = DateTime.Now,
                Email = UserEmail,
                Id = UserId,
                Name = UserName
            };
        }

        public void CreateUserDtoCreate()
        {
            UserDtoCreate = new UserDtoCreate()
            {
                Email = UserEmail,
                Name = UserName
            };
        }

        public void CreateUserCreateResultDTO()
        {
            UserCreateResultDTO = new UserCreateResultDTO()
            {
                CreatedAt = DateTime.Now,
                Email = UserEmail,
                Id = UserId,
                Name = UserName
            };
        }

        public void CreateUserDtoUpdate()
        {
            UserDtoUpdate = new UserDtoUpdate()
            {
                Email = UserEmail,
                Id = UserId,
                Name = UserName
            };
        }

        public void CreateUserUpdateResultDTO()
        {
            UserUpdateResultDTO = new UserUpdateResultDTO()
            {
                Email = UserEmail,
                Id = UserId,
                Name = UserName,
                UpdatedAt = DateTime.Now
            };
        }

        public void CreateUserEntity()
        {
            UserEntity = new UserEntity()
            {
                CreatedAt = DateTime.Now,
                Email = UserEmail,
                Id = UserId,
                Name = UserName,
                UpdatedAt = DateTime.Now.AddDays(3)
            };
        }
    }
}