using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NotificationApi.Models.Dto;
using NotificationApi.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Data.Common;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationApi.Services
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IConfiguration _config;
        private readonly ITwilioService _twilio;

        public RabbitMqConsumer(IConfiguration config, ITwilioService twilio) 
        {
            _twilio = twilio;
            _config = config;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMq:hostName"],
                Port = Int32.Parse(_config["RabbitMq:port"]),
                UserName = _config["RabbitMq:userName"],
                Password = _config["RabbitMq:password"],
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "sms", exclusive: false, durable: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var serializedMessage = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<MessageDto>(serializedMessage);

                _twilio.SendSms(message);
            };

            channel.BasicConsume(queue: "sms", autoAck: true, consumer: consumer);
        }
    }
}
