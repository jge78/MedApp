using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MEDApp.UserManagement.Api.Messaging
{

    public class RabbitMQMessagingService : IMessagingService
    {
        private const string EXCHANGE_NAME = "MedAppUsers";
        private const string QUEUE_NAME = "Users";
        private string _replyQueueName = "rpc_reply";
        private static IConfigurationRoot config;

        public RabbitMQMessagingService()
        {
            Initialize();
        }

        public T Add<T>(T message)
        {
            Message addUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Add,
                id="0",
                payload = message
            };

            var returnValue = JsonConvert.DeserializeObject<T>(Send(addUserMessage));
            return returnValue;
        }

        public T Delete<T>(int id)
        {
            Message deleteUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Delete,
                id = id.ToString()
            };

            var returnValue = JsonConvert.DeserializeObject<T>(Send(deleteUserMessage));
            return returnValue;
        }
        
        public T Get<T>(int id)
        {
            Message getUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Get,
                id = id.ToString()
            };

            var returnValue = JsonConvert.DeserializeObject<T>(Send(getUserMessage));
            return returnValue;
        }

        public List<T> GetAll<T>()
        {
            Message getAllUsersMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.GetAll
            };

            var returnList = JsonConvert.DeserializeObject<List<T>>(Send(getAllUsersMessage));
            return returnList;
        }
        public T Update<T>(T message)
        {
            Message updateUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Update,
                payload = message
            };

            var returnValue = JsonConvert.DeserializeObject<T>(Send(updateUserMessage));
            return returnValue;
        }

        public string Send<T>(T message)
        {
            EventingBasicConsumer _consumer;
            string returnMessage = "";

            var factory = new ConnectionFactory
            {
                HostName = config.GetSection("MessagingServers:Server").Value.ToString(),
                UserName = config.GetSection("MessagingServers:User").Value.ToString(),
                Password = config.GetSection("MessagingServers:Password").Value.ToString()
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);
            channel.QueueDeclare(QUEUE_NAME, true, false, false);
            channel.QueueDeclare(_replyQueueName, true, false, false);
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
                returnMessage = Encoding.UTF8.GetString(body);
            };

            channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: QUEUE_NAME, basicProperties: props, body: body);

            while (returnMessage == "")
            {
                channel.BasicConsume(queue: _replyQueueName, autoAck: true, consumer: _consumer);
            }

            return returnMessage;

        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

        }
    }
}
