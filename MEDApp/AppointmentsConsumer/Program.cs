using System.Text;
using System.Threading.Channels;
using AppointmentsConsumer.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AppointmentsConsumer.Data.Models;
using AppointmentsConsumer.Messaging;


namespace AppointmentManagementConsumer
{
    internal class Program
    {
        private const string EXCHANGE_NAME = "MedAppAppointments";
        private const string QUEUE_NAME = "Appointments";
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
                var idAppointment = messageToprocess.id;

                string responseMessage = "";
                Byte[] responseMessageBytes;

                switch (messageToprocess.operationType)
                {
                    case MessageEnums.OperationTypes.Add:
                        var tempAppointment = JsonConvert.SerializeObject(messageToprocess.payload);
                        Appointment Appointment = JsonConvert.DeserializeObject<Appointment>(tempAppointment);

                        Appointment newAppointment = AddAppointment(Appointment);
                        responseMessage = JsonConvert.SerializeObject(newAppointment);
                        break;

                    case MessageEnums.OperationTypes.Delete:
                        DeleteAppointment(Int32.Parse(idAppointment));
                        Appointment deletedAppointment = new Appointment { Id = Int32.Parse(idAppointment) };
                        responseMessage = JsonConvert.SerializeObject(deletedAppointment);
                        break;

                    case MessageEnums.OperationTypes.Get:
                        Appointment getAppointment = GetAppointment(Int32.Parse(idAppointment));
                        responseMessage = JsonConvert.SerializeObject(getAppointment);
                        break;

                    case MessageEnums.OperationTypes.GetAll:
                        List<Appointment> Appointments = GetAllAppointments();
                        responseMessage = JsonConvert.SerializeObject(Appointments);
                        break;

                    case MessageEnums.OperationTypes.Update:
                        var tempUpdateAppointment = JsonConvert.SerializeObject(messageToprocess.payload);
                        Appointment updateAppointment = JsonConvert.DeserializeObject<Appointment>(tempUpdateAppointment);

                        Appointment updated = UpdateAppointment(updateAppointment);
                        responseMessage = JsonConvert.SerializeObject(updated);
                        break;

                    default:
                        break;
                }

                responseMessageBytes = Encoding.UTF8.GetBytes(responseMessage);

                channel.BasicPublish(EXCHANGE_NAME, properties.ReplyTo, replyproperties, responseMessageBytes);
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            Console.ReadKey();

        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

        }

        private static Appointment AddAppointment(Appointment Appointment)
        {
            IAppointmentRepository appointmentRepository = CreateappointmentRepository();
            return appointmentRepository.Add(Appointment);
        }

        private static void DeleteAppointment(int id)
        {
            IAppointmentRepository appointmentRepository = CreateappointmentRepository();
            appointmentRepository.Delete(id);
        }

        private static Appointment GetAppointment(int id)
        {
            IAppointmentRepository appointmentRepository = CreateappointmentRepository();
            return appointmentRepository.Get(id);
        }

        private static List<Appointment> GetAllAppointments()
        {
            IAppointmentRepository appointmentRepository = CreateappointmentRepository();
            return appointmentRepository.GetAll();
        }

        private static Appointment UpdateAppointment(Appointment Appointment)
        {
            IAppointmentRepository appointmentRepository = CreateappointmentRepository();
            return appointmentRepository.Update(Appointment);
        }

        private static IAppointmentRepository CreateappointmentRepository()
        {
            return new AppointmentRepository(config.GetConnectionString("MedAppAppointmentDB"));
        }

    }
}
