using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NotificationApi.Models.Dto;
using NotificationApi.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationApi.Services
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _config;

        public RabbitMqConsumer(IConfiguration config) 
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _config = config;
        }

        public void StartConsuming()
        {
            _channel.QueueDeclare(queue: "sms", exclusive: false, durable: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var serializedMessage = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<MessageDto>(serializedMessage);

                SendSms(message);
            };

            _channel.BasicConsume(queue: "sms", autoAck: true, consumer: consumer);
        }

        public void SendSms(MessageDto messageDto)
        {
            string accountSid = _config["Twilio:accountSid"];
            string authToken = _config["Twilio:authToken"];

            var body = $"Hello! Your verification code is {messageDto.Code}";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber(_config["Twilio:fromPhoneNumber"]),
                to: new Twilio.Types.PhoneNumber(messageDto.PhoneNumber)
            );
        }
    }
}
