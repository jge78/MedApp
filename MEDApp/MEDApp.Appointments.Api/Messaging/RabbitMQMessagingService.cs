using MEDApp.Appointments.Api.Messaging;
using MEDApp.Appointments.Api.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MEDApp.Appointments.Api.Messaging
{

    public class RabbitMQMessagingService : IMessagingService
    {
        private const string EXCHANGE_NAME = "MedAppAppointments";
        private const string QUEUE_NAME = "MedAppAppointments";
        private string _replyQueueName = "rpc_reply";
        private EventingBasicConsumer _consumer;

        public string AddAppointment<T>(T message)
        {
            Message addappointmentMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Add,
                id="0",
                payload = message
            };

            var appointment = new Appointment();
            appointment = JsonConvert.DeserializeObject<Appointment>(Send(addappointmentMessage));
            return appointment.Id.ToString();
        }

        public string DeleteAppointment(int id)
        {
            //throw new NotImplementedException();
            Message deleteappointmentMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Delete,
                id = id.ToString()
            };

            var appointment = new Appointment();
            appointment = JsonConvert.DeserializeObject<Appointment>(Send(deleteappointmentMessage));
            return appointment.Id.ToString();

        }
        
        public Appointment GetAppointment(int id)
        {
            //throw new NotImplementedException();
            Message getappointmentMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Get,
                id = id.ToString()
            };

            var appointment = new Appointment();
            appointment = JsonConvert.DeserializeObject<Appointment>(Send(getappointmentMessage));
            return appointment;

        }

        public List<Appointment> GetAllAppointment()
        {

            //throw new NotImplementedException();
            Message getAllappointmentsMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.GetAll
            };

            var appointments = new List<Appointment>();
            appointments = JsonConvert.DeserializeObject<List<Appointment>>(Send(getAllappointmentsMessage));
            return appointments;

        }
        public Appointment UpdateAppointment<T>(T message)
        {
            Message updateappointmentMessage = new Message
            {
                operationType = MessageEnums.OperationTypes.Update,
                payload = message
            };

            var updateappointment = new Appointment();
            updateappointment = JsonConvert.DeserializeObject<Appointment>(Send(updateappointmentMessage));
            return updateappointment;

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
