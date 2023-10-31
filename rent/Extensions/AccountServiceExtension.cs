using Microsoft.Extensions.DependencyInjection;
using PersonApi.Services;

namespace PersonApi.Extensions
{
    public static class AccountServiceExtension
    {
        public static void AddAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();
        }
    }
}
