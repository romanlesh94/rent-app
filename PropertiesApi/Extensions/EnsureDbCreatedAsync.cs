using HouseApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace HouseApi.Extensions
{
    public static class EnsureDbCreatedExtension
    {
        public static async Task EnsureDbCreatedAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var appContext = scope.ServiceProvider.GetRequiredService<HouseDbContext>();

            await appContext.Database.MigrateAsync();
        }
    }
}
