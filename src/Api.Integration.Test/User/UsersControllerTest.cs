using Domain.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.User
{
    public class UsersControllerTest : BaseIntegration
    {
        [Fact]
        public async Task Crud_User()
        {
            await AddToken();

            Guid userId = await Post();

            await GetAll();

            await GetById(userId);

            await Put(userId);

            await Delete(userId);
        }

        private async Task<Guid> Post()
        {
            var user = new UserDtoCreate()
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName()
            };

            var stringContent = GetStringContent(user);

            var response = await HttpClient.PostAsync($"{HostApi}{"users"}", stringContent);

            var result = await DeserializeObject<UserCreateResultDTO>(response);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Name);
            Assert.NotEmpty(result.Email);
            Assert.NotNull(result.CreatedAt);

            return result.Id;
        }

        private async Task GetAll()
        {
            var response = await HttpClient.GetAsync($"{HostApi}users");

            var result = await DeserializeObject<IList<UserDto>>(response);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        private async Task GetById(Guid id)
        {
            var response = await HttpClient.GetAsync($"{HostApi}users/{id}");

            var result = await DeserializeObject<UserDto>(response);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.NotEmpty(result.Email);
            Assert.NotEmpty(result.Name);
        }

        private async Task Put(Guid id)
        {
            var user = new UserDtoUpdate()
            {
                Email = Faker.Internet.Email(),
                Id = id,
                Name = Faker.Name.FullName()
            };

            var stringContent = GetStringContent(user);

            var response = await HttpClient.PutAsync($"{HostApi}users", stringContent);

            var result = await DeserializeObject<UserUpdateResultDTO>(response);

            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
        }
        
        private async Task Delete(Guid id)
        {
            var response = await HttpClient.DeleteAsync($"{HostApi}users/{id}");

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }
    }
}
