using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonApi.Repository;
using System.Threading.Tasks;

namespace PersonApi.Extensions
{
    public static class EnsureDbCreatedExtension
    {
        public static async Task EnsureDbCreatedAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var appContext = scope.ServiceProvider.GetRequiredService<PersonDbContext>();

            await appContext.Database.MigrateAsync();
        }
    }
}
