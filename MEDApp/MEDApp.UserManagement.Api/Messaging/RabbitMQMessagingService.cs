using MEDApp.UserManagement.Api.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MEDApp.UserManagement.Api.Messaging
{

    public class RabbitMQMessagingService : IMessagingService
    {
        private const string EXCHANGE_NAME = "MedApp";
        private const string QUEUE_NAME = "Users";
        private string _replyQueueName = "rpc_reply";
        private EventingBasicConsumer _consumer;

        public string AddUser<T>(T message)
        {
            Message addUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Add,
                id="0",
                payload = message
            };

            var user = new User();
            user = JsonConvert.DeserializeObject<User>(Send(addUserMessage));
            return user.Id.ToString();
        }

        public string DeleteUser(int id)
        {
            //throw new NotImplementedException();
            Message deleteUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Delete,
                id = id.ToString()
            };

            var user = new User();
            user = JsonConvert.DeserializeObject<User>(Send(deleteUserMessage));
            return user.Id.ToString();

        }
        
        public User GetUser(int id)
        {
            //throw new NotImplementedException();
            Message getUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Get,
                id = id.ToString()
            };

            var user = new User();
            user = JsonConvert.DeserializeObject<User>(Send(getUserMessage));
            return user;

        }

        public List<User> GetAllUsers()
        {

            //throw new NotImplementedException();
            Message getAllUsersMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.GetAll
            };

            var users = new List<User>();
            users = JsonConvert.DeserializeObject<List<User>>(Send(getAllUsersMessage));
            return users;

        }
        public User UpdateUser<T>(T message)
        {
            Message updateUserMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Update,
                payload = message
            };

            var updateUser = new User();
            updateUser = JsonConvert.DeserializeObject<User>(Send(updateUserMessage));
            return updateUser;

        }

        public string Send<T>(T message)
        {
            string returnMessage = "";

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
                returnMessage = Encoding.UTF8.GetString(body);
            };

            channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: QUEUE_NAME, basicProperties: props, body: body);

            while (returnMessage == "")
            {
                channel.BasicConsume(queue: _replyQueueName, autoAck: true, consumer: _consumer);
            }

            return returnMessage;

        }

    }
}
