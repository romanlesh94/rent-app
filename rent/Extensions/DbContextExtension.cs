using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace rent.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();
        }
    }
}
