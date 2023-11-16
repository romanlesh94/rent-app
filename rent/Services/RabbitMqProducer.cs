using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PersonApi.Models.Dto;
using PersonApi.Repository;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly IConfiguration _config;

        public RabbitMqProducer(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendSmsMessage(MessageDto message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMq:hostName"],
                Port = Int32.Parse(_config["RabbitMq:port"]),
                UserName = _config["RabbitMq:userName"],
                Password = _config["RabbitMq:password"],
            };
            
            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "sms", exclusive: false, durable: false, autoDelete: false, arguments: null);

            var serializedMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(serializedMessage);

            channel.BasicPublish(exchange: "", routingKey: "sms", basicProperties: null, body: body);

        }
    }
}
