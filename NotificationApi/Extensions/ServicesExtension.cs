using Microsoft.Extensions.DependencyInjection;
using NotificationApi.Services;

namespace NotificationApi.Extensions
{
    public static class ServicesExtension
    {
        public static void AddNotificationServices(this IServiceCollection services)
        {
            services.AddSingleton<ITwilioService, TwilioService>();
            services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
        }
    }
}
