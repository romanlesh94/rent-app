using Microsoft.Extensions.DependencyInjection;
using PersonApi.Repository;

namespace PersonApi.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PersonDbContext>();
        }
    }
}
