using PersonApi.Models.Dto;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface ITwilioService
    {
        Task SendSmsAsync(SmsDto smsDto);
    }
}
