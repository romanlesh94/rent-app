using HouseApi.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HouseApi.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IHouseRepository, HouseRepository>();
        }
    }
}
