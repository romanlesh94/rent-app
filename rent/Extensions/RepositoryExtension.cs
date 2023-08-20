using Microsoft.Extensions.DependencyInjection;
using PersonService.Repository;

namespace PersonService.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
