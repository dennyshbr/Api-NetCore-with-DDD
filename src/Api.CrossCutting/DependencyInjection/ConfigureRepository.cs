using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection service)
        {
            service.AddDbContext<MyContext>(
                options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=dbApicomDDD;Integrated Security=true")
            );

            service.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}
