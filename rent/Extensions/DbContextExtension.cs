using Microsoft.Extensions.DependencyInjection;
using PersonService.Repository;

namespace PersonService.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PersonDbContext>();
        }
    }
}
