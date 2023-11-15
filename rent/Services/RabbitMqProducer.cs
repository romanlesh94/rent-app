using Newtonsoft.Json;
using PersonApi.Models.Dto;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        public async Task SendSmsMessage(MessageDto message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
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
