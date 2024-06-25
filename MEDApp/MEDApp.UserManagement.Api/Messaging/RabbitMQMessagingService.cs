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
        private string _replyQueueName = "rpc_reply";
        private EventingBasicConsumer _consumer;

        public string SendMessage<T>(T message)
        {
            //Boolean valorRecuperado = false;
            string idNuevoUsuario = "";

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);

            channel.QueueBind(queue: _replyQueueName, exchange: EXCHANGE_NAME, routingKey: _replyQueueName);
            _consumer = new EventingBasicConsumer(channel);

            var corrId = Guid.NewGuid().ToString();
            var props = channel.CreateBasicProperties();
            props.ReplyTo = _replyQueueName;
            props.CorrelationId = corrId;

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            _consumer.Received += (model, eventArgs) =>
            {
                if (eventArgs.BasicProperties.ReplyTo != null)
                    return;
                
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                User user = JsonConvert.DeserializeObject<User>(message);

                //valorRecuperado=true;
                idNuevoUsuario = user.Id.ToString();
            };

            channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: QUEUE_NAME, basicProperties: props, body: body);

            while (idNuevoUsuario=="")
            {
                channel.BasicConsume(queue: _replyQueueName, autoAck: true, consumer: _consumer);
            }

            return idNuevoUsuario;


        }

    }
}
