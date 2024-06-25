using MEDApp.UserManagement.Api.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;

namespace MEDApp.UserManagement.Api.Messaging
{
    
    public class RabbitMQMessagingService : IMessagingService
    {
        private const string EXCHANGE_NAME = "MedApp";
        private const string QUEUE_NAME = "Users";
       

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);
            channel.QueueDeclare(QUEUE_NAME, exclusive: false);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: QUEUE_NAME, body: body);

        }

    }
}
