using Microsoft.Extensions.DependencyInjection;
using PersonService.Services;

namespace PersonService.Extensions
{
    public static class AccountServiceExtension
    {
        public static void AddAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
