using Microsoft.Extensions.Configuration;
using PersonApi.Models.Dto;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;

namespace PersonApi.Services
{
    public class TwilioService
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

        public async Task<string> CheckSmsCodeAsync(CheckSmsCodeDto checkSmsCodeDto)
        {
            string accountSid = _config["Twilio:accountSid"];
            string authToken = _config["Twilio:authToken"];

            TwilioClient.Init(accountSid, authToken);

            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: checkSmsCodeDto.PhoneNumber,
                code: checkSmsCodeDto.Code,
                pathServiceSid: accountSid
            );

            return verificationCheck.Status;
        }
    }
}
