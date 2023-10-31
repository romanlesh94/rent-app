using Microsoft.Extensions.DependencyInjection;
using NotificationApi.Services;

namespace NotificationApi.Extensions
{
    public static class ServicesExtension
    {
        public static void AddNotificationServices(this IServiceCollection services)
        {
            services.AddScoped<ITwilioService, TwilioService>();
            services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
        }
    }
}
