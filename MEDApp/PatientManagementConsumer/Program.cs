using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using PatientManagementConsumer.Messaging;
using PatientManagementConsumer;
//using PatientManagementConsumer.Data;

namespace AppointmentManagementConsumer
{
    internal class Program
    {
        private const string EXCHANGE_NAME = "MedAppAppointmentHistory";
        private const string QUEUE_NAME = "AppointmentHistory";
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
            string responseMessage="";

            consumer.Received += (model, EventArgs) =>
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

                var consumer = ConsumerProcessorFactory.Create(messageToprocess.messageEntity, config.GetConnectionString("MedAppAppointmentDB"));

                responseMessage = consumer.ProcessMessage(messageToprocess);

                Byte[] responseMessageBytes;
                responseMessageBytes = Encoding.UTF8.GetBytes(responseMessage);

                channel.BasicPublish(EXCHANGE_NAME, properties.ReplyTo, replyproperties, responseMessageBytes);
            };

            while (true)
            {
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }

        }
        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

            //Initialize Database
            //IAppointmentRepository appointmentRepository = new AppointmentRepository(config.GetConnectionString("MasterDB"));

            //if (appointmentRepository.InitializeDB() == false)
            //{
            //    throw new Exception("The Database could not be initialized");
            //}

        }

    }
}
