using System.Threading.Tasks;

namespace NotificationApi.Services
{
    public interface IRabbitMqConsumer
    {
        void StartConsuming();
    }
}
