using NotificationApi.Models.Dto;
using System.Threading.Tasks;

namespace NotificationApi.Services
{
    public interface ITwilioService
    {
        Task SendSmsAsync(SmsDto smsDto);
    }
}
