using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Domain.DTOs.User;
using Faker;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Controllers
{
    public class UsersControllerTest
    {
        private Mock<IUserService> _mockService;
        private UsersController _userController;

        public UsersControllerTest()
        {
            _mockService = new Mock<IUserService>();
            _userController = new UsersController(_mockService.Object);
        }

        [Fact]
        public async void UsersController_GetAll_ReturnOk()
        {
            IList<UserDto> usersDto = new List<UserDto>();

            _mockService
                .Setup(m => m.GetAll())
                .Returns(Task.FromResult(usersDto));

            //Act
            var users = await _userController.GetAll();

            Assert.True(users is OkObjectResult);
        }

        [Fact]
        public async void UsersController_GetAll_ReturnObjectResultStatusCode500()
        {
            //Arrange

            _mockService
                .Setup(m => m.GetAll())
                .Throws(new Exception("Test Exception"));

            //Act
            var result = await _userController.GetAll();

            //Assert
            Assert.True(result is ObjectResult);
            Assert.True(((ObjectResult)result).StatusCode == 500);
        }

        [Fact]
        public async void UsersController_GetById_ReturnOk()
        {
            UserDto userDto = new UserDto();

            _mockService
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(userDto));

            //Act
            var users = await _userController.GetById(Guid.NewGuid());

            Assert.True(users is OkObjectResult);
        }

        [Fact]
        public async void UsersController_GetById_ReturnObjectResultStatusCode500()
        {
            //Arrange

            _mockService
                .Setup(m => m.Get(It.IsAny<Guid>()))
                .Throws(new Exception("Test Exception"));

            //Act
            var result = await _userController.GetById(Guid.NewGuid());

            //Assert
            Assert.True(result is ObjectResult);
            Assert.True(((ObjectResult)result).StatusCode == 500);
        }

        [Fact]
        public async void UsersController_Post_ReturnCreated()
        {
            //Arrange

            _mockService
                .Setup(s => s.Post(It.IsAny<UserDtoCreate>()))
                .Returns(Task.FromResult(new UserCreateResultDTO()
                {
                    CreatedAt = DateTime.Now,
                    Email = Internet.Email(),
                    Id = Guid.NewGuid(),
                    Name = Name.FullName()
                }));

            Mock<IUrlHelper> mockUrl = new Mock<IUrlHelper>();
            mockUrl
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://localhost:5000");

            _userController.Url = mockUrl.Object;

            //Act
            var result = await _userController.Post(new UserDtoCreate());

            //Assert
            Assert.True(result is CreatedResult);
        }
        
        [Fact]
        public async void UsersController_Post_ReturnObjectResultStatusCode500()
        {
            //Arrange

            _mockService
                .Setup(s => s.Post(It.IsAny<UserDtoCreate>()))
                .Throws(new Exception("Test Exception"));

            //Act
            var result = await _userController.Post(new UserDtoCreate());

            //Assert
            Assert.True(result is ObjectResult);
            Assert.True(((ObjectResult)result).StatusCode == 500);
        }

        [Fact]
        public async void UsersController_Post_ReturnBadRequest()
        {
            //Arrange
            UserCreateResultDTO userCreateResultDTO = null;

            _mockService
                .Setup(s => s.Post(It.IsAny<UserDtoCreate>()))
                .Returns(Task.FromResult(userCreateResultDTO));

            //Act
            var result = await _userController.Post(new UserDtoCreate());

            //Assert
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async void UsersController_Put_ReturnOk()
        {
            //Arrange

            _mockService
                .Setup(s => s.Put(It.IsAny<UserDtoUpdate>()))
                .Returns(Task.FromResult(new UserUpdateResultDTO()
                {
                    UpdatedAt = DateTime.Now,
                    Email = Internet.Email(),
                    Id = Guid.NewGuid(),
                    Name = Name.FullName()
                }));


            //Act
            var result = await _userController.Put(new UserDtoUpdate());

            //Assert
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async void UsersController_Put_ReturnObjectResultStatusCode500()
        {
            //Arrange

            _mockService
                .Setup(s => s.Put(It.IsAny<UserDtoUpdate>()))
                .Throws(new Exception("Test Exception"));

            //Act
            var result = await _userController.Put(new UserDtoUpdate());

            //Assert
            Assert.True(result is ObjectResult);
            Assert.True(((ObjectResult)result).StatusCode == 500);
        }

        [Fact]
        public async void UsersController_Put_ReturnBadRequest()
        {
            //Arrange
            UserUpdateResultDTO userUpdateResultDTO = null;

            _mockService
                .Setup(s => s.Put(It.IsAny<UserDtoUpdate>()))
                .Returns(Task.FromResult(userUpdateResultDTO));

            //Act
            var result = await _userController.Put(new UserDtoUpdate());

            //Assert
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async void UsersController_Delete_ReturnNonContent()
        {
            //Arrange

            _mockService
                .Setup(s => s.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            //Act
            var result = await _userController.Delete(Guid.NewGuid());

            //Assert
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public async void UsersController_Delete_ReturnObjectResultStatusCode500()
        {
            //Arrange

            _mockService
                .Setup(s => s.Delete(It.IsAny<Guid>()))
                .Throws(new Exception("Test Exception"));

            //Act
            var result = await _userController.Delete(Guid.NewGuid());

            //Assert
            Assert.True(result is ObjectResult);
            Assert.True(((ObjectResult)result).StatusCode == 500);
        }

        [Fact]
        public async void UsersController_Delete_ReturnBadRequest()
        {
            //Arrange

            _mockService
                .Setup(s => s.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            //Act
            var result = await _userController.Delete(Guid.NewGuid());

            //Assert
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async void UsersController_TestModelStateIsValid_ReturnBadRequest()
        {
            _userController.ModelState.AddModelError("Id", "Invalid Format");

            var resultGetById = await _userController.GetById(Guid.NewGuid());
            var resultPost = await _userController.Post(new UserDtoCreate());
            var resultPut = await _userController.Put(new UserDtoUpdate());
            var resultDelete = await _userController.Delete(Guid.NewGuid());

            Assert.True(resultGetById is BadRequestObjectResult);
            Assert.True(resultPost is BadRequestObjectResult);
            Assert.True(resultPut is BadRequestObjectResult);
            Assert.True(resultDelete is BadRequestObjectResult);
        }
    }
}
