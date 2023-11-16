using NotificationApi.Models.Dto;
using System.Threading.Tasks;

namespace NotificationApi.Services
{
    public interface ITwilioService
    {
        void SendSms(MessageDto messageDto);
    }
}
