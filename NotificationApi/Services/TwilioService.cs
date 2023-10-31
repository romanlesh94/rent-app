using Microsoft.Extensions.Configuration;
using NotificationApi.Models.Dto;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;

namespace NotificationApi.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly IConfiguration _config;

        public TwilioService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendSmsAsync(SmsDto smsDto)
        {
            string accountSid = _config["Twilio:accountSid"];
            string authToken = _config["Twilio:authToken"];

            TwilioClient.Init(accountSid, authToken);

            var message = await MessageResource.CreateAsync(
                body: smsDto.Message,
                from: new Twilio.Types.PhoneNumber(_config["Twilio:fromPhoneNumber"]),
                to: new Twilio.Types.PhoneNumber(smsDto.PhoneNumber)
            );
        }


    }
}
