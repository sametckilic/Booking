using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Booking.Application.Interfaces.Queue
{
    public interface IRabbitmqFactory
    {
        public void SendMessage<T>(T message, string queueName,string exchangeName, string exchangeType = "direct");

        public EventingBasicConsumer Receive<T>(Action<T> act, string queueName, string exchangeName, string exchangeType = "direct");
    }
}
