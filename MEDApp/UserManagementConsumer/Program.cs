using MEDApp.UserManagement.Api.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UserManagementConsumer.Data;
using UserManagementConsumer.Messaging;

namespace UserManagementConsumer
{
    internal class Program
    {
        private const string EXCHANGE_NAME = "MedAppUsers";
        private const string QUEUE_NAME = "Users";
        private static IConfigurationRoot config;

        static void Main(string[] args)
        {
            Initialize();

            var factory = new ConnectionFactory
            {
                HostName = config.GetSection("MessagingServers:Server").Value.ToString(),
                UserName = config.GetSection("MessagingServers:User").Value.ToString(),
                Password = config.GetSection("MessagingServers:Password").Value.ToString()
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: EXCHANGE_NAME, routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, EventArgs) =>
            {
                var body = EventArgs.Body.ToArray();
                var properties = EventArgs.BasicProperties;

                if (properties.ReplyTo == null)
                    return;

                var replyproperties = channel.CreateBasicProperties();
                replyproperties.CorrelationId = properties.CorrelationId;

                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

                Message messageToprocess = JsonConvert.DeserializeObject<Message>(message);
                var idUser = messageToprocess.id;

                string responseMessage = "";
                Byte[] responseMessageBytes;

                switch (messageToprocess.operationType)
                {
                    case MessageEnums.OperationTypes.Add:
                        var tempUser = JsonConvert.SerializeObject(messageToprocess.payload);
                        User user = JsonConvert.DeserializeObject<User>(tempUser);

                        User newUser = await AddUser(user);
                        responseMessage = JsonConvert.SerializeObject(newUser);
                        break;

                    case MessageEnums.OperationTypes.Delete:
                        var deleteResult = await DeleteUser(Int32.Parse(idUser));
                        responseMessage = JsonConvert.SerializeObject(deleteResult);
                        break;

                    case MessageEnums.OperationTypes.Get:
                        User getUser = await GetUser(Int32.Parse(idUser));
                        responseMessage = JsonConvert.SerializeObject(getUser);
                        break;

                    case MessageEnums.OperationTypes.GetAll:
                        List<User> users = await GetAllUsers();
                        responseMessage = JsonConvert.SerializeObject(users);
                        break;

                    case MessageEnums.OperationTypes.Update:
                        var tempUpdateUser = JsonConvert.SerializeObject(messageToprocess.payload);
                        User updateUser = JsonConvert.DeserializeObject<User>(tempUpdateUser);

                        User updated = await UpdateUser(updateUser);
                        responseMessage = JsonConvert.SerializeObject(updated);
                        break;

                    default:
                        break;
                }

                responseMessageBytes = Encoding.UTF8.GetBytes(responseMessage);

                channel.BasicPublish(EXCHANGE_NAME, properties.ReplyTo, replyproperties, responseMessageBytes);
            };

            while (true)
            {
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }

        }

        private static async void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

            //Initialize Database
            IUserRepository userRepository = new UserRepository(config.GetConnectionString("MasterDB"));
            var initializeResult = await userRepository.InitializeDB();
            if (initializeResult == false)
            {
                throw new Exception("The Database could not be initialized");
            }

        }

        private static async Task<User> AddUser(User user)
        {
            IUserRepository userRepository = CreateUserRepository();
            return await userRepository.Add(user);
        }

        private static async Task<bool> DeleteUser(int id)
        {
            IUserRepository userRepository = CreateUserRepository();
            return await userRepository.Delete(id);
        }

        private static async Task<User> GetUser(int id)
        {
            IUserRepository userRepository = CreateUserRepository();
            return await userRepository.GetUser(id);
        }

        private static async Task<List<User>> GetAllUsers()
        {
            IUserRepository userRepository = CreateUserRepository();
            return await userRepository.GetAll();
        }

        private static async Task<User> UpdateUser(User user)
        {
            IUserRepository userRepository = CreateUserRepository();
            return await userRepository.Update(user);
        }

        private static IUserRepository CreateUserRepository()
        {
            return new UserRepository(config.GetConnectionString("MedAppUserDB"));
        }

    }
}
