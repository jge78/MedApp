using System.Text;
using MEDApp.UserManagement.Api.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserManagementConsumer.Data;

namespace UserManagementConsumer
{
    internal class Program
    {
        private const string EXCHANGE_NAME = "MedApp";
        private const string QUEUE_NAME = "Users";
        private static IConfigurationRoot config;

        static void Main(string[] args)
        {
            Initialize();

            var factory = new ConnectionFactory
            {
                HostName = config.GetSection("MessagingServers:UsersManagement").Value.ToString()
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: EXCHANGE_NAME, routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, EventArgs) =>
            {
                var body = EventArgs.Body.ToArray();
                var properties = EventArgs.BasicProperties;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

                User user = JsonConvert.DeserializeObject<User>(message);
                User newUser = AddUser(user);

                var responseMessage = JsonConvert.SerializeObject(newUser);
                var responseMessageeBytes = Encoding.UTF8.GetBytes(responseMessage);

            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            Console.ReadKey();

        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

        }

        private static User AddUser(User user)
        {
            IUserRepository userRepository = CreateUserRepository();
            return userRepository.Add(user);
        }

        private static IUserRepository CreateUserRepository()
        {
            return new UserRepository(config.GetConnectionString("MedAppUserDB"));
        }

    }
}
