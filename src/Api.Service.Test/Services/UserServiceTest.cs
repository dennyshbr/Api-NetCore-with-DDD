using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Service.Services;
using Api.Service.Test.User;
using Domain.DTOs.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Services
{
    public class UserServiceTest : BaseTestService
    {
        private Mock<IRepository<UserEntity>> _mockRepository;
        private UserService _userService;
        private UserFake _userFake;

        public UserServiceTest() : base()
        {
            _mockRepository = new Mock<IRepository<UserEntity>>();
            _userFake = new UserFake();
            _userService = new UserService(_mockRepository.Object, Mapper);
        }
        
        [Fact]
        public async void UserService_ReturnTrueForDelete()
        {
            //Arrange
            _mockRepository
                .Setup(m => m.DeleteAsync(_userFake.UserId))
                .Returns(Task.FromResult(true));

            //Act
            bool deleted = await _userService.Delete(_userFake.UserId);

            Assert.True(deleted);
        }

        [Fact]
        public async void UserService_ReturnUserDtoInGet()
        {
            //Arrange
            var userEntity = new UserEntity()
            {
                CreatedAt = DateTime.Now,
                Email = _userFake.UserEmail,
                Id = _userFake.UserId,
                Name = _userFake.UserName,
                UpdatedAt = null
            };

            _mockRepository
                .Setup(m => m.GetAsync(_userFake.UserId))
                .Returns(Task.FromResult(userEntity));

            //Act
            UserDto userDto = await _userService.Get(_userFake.UserId);

            Assert.NotNull(userDto);
        }

        [Fact]
        public async void UserService_ReturnListUserDtoInGetAll()
        {
            //Arrange

            _mockRepository
                .Setup(m => m.GetAllAsync())
                .Returns(Task.FromResult(_userFake.ListUserEntity));

            //Act
            var listUserDto = await _userService.GetAll();

            Assert.Equal(_userFake.ListUserEntity.Count, listUserDto.Count);
        }

        [Fact]
        public async void UserService_ReturnUserCreateResultDTOInPost()
        {
            _mockRepository
                .Setup(m => m.InsertAsync(It.IsAny<UserEntity>()))
                .Returns(Task.FromResult(_userFake.UserEntity));

            //Act
            var result = await _userService.Post(_userFake.UserDtoCreate);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Name);
            Assert.NotEmpty(result.Email);
        }

        [Fact]
        public async void UserService_ReturnUserUpdateResultDTOInPut()
        {
            _mockRepository
                .Setup(m => m.UpdateAsync(It.IsAny<UserEntity>()))
                .Returns(Task.FromResult(_userFake.UserEntity));

            //Act
            var result = await _userService.Put(_userFake.UserDtoUpdate);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Name);
            Assert.NotEmpty(result.Email);
        }
    }
}
