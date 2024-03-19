using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Queue;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Booking.Persistence.Queue
{
    public class RabbitmqFactory : IRabbitmqFactory
    {
        private readonly IConfiguration configuration;

        public RabbitmqFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendMessage<T>(T message, string queueName, string exchangeName ,string exchangeType = "direct")
        {

            var factory = new ConnectionFactory() { HostName = configuration.GetConnectionString("RabbitMQ") };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);

            channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, null);

            channel.QueueBind(queueName, exchangeName, queueName);

            var jsonString = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(exchange: exchangeName, routingKey: queueName, body: body, basicProperties: null);
        }


        public EventingBasicConsumer Receive<T>(Action<T> action, string queueName, string exchangeName, string exchangeType = "direct")
        {
            var factory = new ConnectionFactory() { HostName = configuration.GetConnectionString("RabbitMQ") };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);

            channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, null);

            channel.QueueBind(queueName, exchangeName, queueName);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (m, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var model = JsonSerializer.Deserialize<T>(message);

                action(model);

                consumer.Model.BasicAck(eventArgs.DeliveryTag, multiple: false);

            };

            consumer.Model.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            return consumer;
        }
    }
}
