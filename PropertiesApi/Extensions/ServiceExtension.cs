using HouseApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HouseApi.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHouseService, HouseService>();
        }
    }
}
