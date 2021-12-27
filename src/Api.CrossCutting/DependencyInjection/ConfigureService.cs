using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<ILoginService, LoginService>();
        }
    }
}
