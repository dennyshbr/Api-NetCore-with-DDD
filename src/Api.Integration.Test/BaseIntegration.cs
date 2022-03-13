using Api.Data.Context;
using Application;
using AutoMapper;
using CrossCutting.Mappings;
using Domain.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext Context { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public IMapper Mapper { get; private set; }
        public string HostApi { get; private set; }
        public HttpResponseMessage Response { get; set; }

        public BaseIntegration()
        {
            HostApi = "http://localhost:5000/api/";

            var builder = new WebHostBuilder()
                                .UseEnvironment("Testing")
                                .UseStartup<Startup>();

            var server = new TestServer(builder);

            Context = server.Host.Services.GetService(typeof(MyContext)) as MyContext;

            Mapper = new AutoMapperFixture().GetMapper();

            HttpClient = server.CreateClient();
        }

        public async Task AddToken()
        {
            var loginDto = new LoginDTO()
            {
                Email = "adm@email.com"
            };

            var stringContext = GetStringContent(loginDto);

            var result = await HttpClient.PostAsync($"{HostApi}login", stringContext);
            var jsonLogin = await result.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDTO>(jsonLogin);

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.acessToken);
        }

        public StringContent GetStringContent(object dataClass)
        {
            return new StringContent(JsonConvert.SerializeObject(dataClass), Encoding.UTF8, "application/json");
        }

        public async Task<T> DeserializeObject<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        public void Dispose()
        {
            Context.Dispose();
            HttpClient.Dispose();
        }
    }

    public class AutoMapperFixture
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new DtoToEntityProfile());
            });

            return config.CreateMapper();
        }
    }
}
