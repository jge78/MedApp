using AppointmentsConsumer.Messaging;
using Newtonsoft.Json;
using AppointmentsConsumer.Data.Models;
using AppointmentsConsumer.Data;

namespace AppointmentsConsumer
{
    public class AppointmentConsumerProcessor :ConsumerProcessor
    {
        private readonly AppointmentRepository appointmentRepository;

        public AppointmentConsumerProcessor(string dataConectionString) :base(dataConectionString)
        {
            appointmentRepository = new AppointmentRepository(dataConectionString);
        }

        public override String ProcessMessage(Message messageToprocess)
        {
            string responseMessage;
            var idAppointment = messageToprocess.id;

            switch (messageToprocess.messageOperation)
            {
                case MessageEnums.MessageOperations.Add:
                    var tempAppointment = JsonConvert.SerializeObject(messageToprocess.payload);
                    Appointment appointment = JsonConvert.DeserializeObject<Appointment>(tempAppointment);

                    Appointment newAppointment = appointmentRepository.Add(appointment);
                    responseMessage = JsonConvert.SerializeObject(newAppointment);
                    break;

                case MessageEnums.MessageOperations.Delete:
                    appointmentRepository.Delete(Int32.Parse(idAppointment));
                    Appointment deletedAppointment = new() { Id = Int32.Parse(idAppointment) };
                    responseMessage = JsonConvert.SerializeObject(deletedAppointment);
                    break;

                case MessageEnums.MessageOperations.Get:
                    Appointment getAppointment = appointmentRepository.Get(Int32.Parse(idAppointment));
                    responseMessage = JsonConvert.SerializeObject(getAppointment);
                    break;

                case MessageEnums.MessageOperations.GetAll:
                    List<Appointment> Appointments = appointmentRepository.GetAll();
                    responseMessage = JsonConvert.SerializeObject(Appointments);
                    break;

                case MessageEnums.MessageOperations.Update:
                    var tempUpdateAppointment = JsonConvert.SerializeObject(messageToprocess.payload);
                    Appointment updateAppointment = JsonConvert.DeserializeObject<Appointment>(tempUpdateAppointment);

                    Appointment updated = appointmentRepository.Update(updateAppointment);
                    responseMessage = JsonConvert.SerializeObject(updated);
                    break;

                default:
                    responseMessage = "";
                    break;
            }

            return responseMessage;

        }

    }
}
