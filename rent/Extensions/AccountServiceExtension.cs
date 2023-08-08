using Microsoft.Extensions.DependencyInjection;
using rent.Services;

namespace rent.Extensions
{
    public static class AccountServiceExtension
    {
        public static void AddAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
