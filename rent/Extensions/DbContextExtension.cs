using Microsoft.Extensions.DependencyInjection;
using rent.Repository;

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
