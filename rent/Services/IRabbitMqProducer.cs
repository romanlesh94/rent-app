using PersonApi.Models.Dto;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface IRabbitMqProducer
    {
        Task SendSmsMessage(MessageDto message);
    }
}
