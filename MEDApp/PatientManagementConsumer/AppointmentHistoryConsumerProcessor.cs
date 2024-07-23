using Newtonsoft.Json;
using PatientManagementConsumer.Data;
using PatientManagementConsumer.Data.Models;
using PatientManagementConsumer.Messaging;

namespace PatientManagementConsumer
{
    internal class AppointmentHistoryConsumerProcessor : ConsumerProcessor
    {
        private IAppointmentHistoryRepository appointmentHistoryRepository;
        public AppointmentHistoryConsumerProcessor(string dataConectionString) : base(dataConectionString)
        {
            appointmentHistoryRepository = new AppointmentHistoryRepository();
        }
        public override string ProcessMessage(Message messageToprocess)
        {
            string responseMessage;
            var idAppointment = messageToprocess.id;


            switch (messageToprocess.messageOperation)
            {
                case MessageEnums.MessageOperations.Add:
                    var tempAppointmentHistory = JsonConvert.SerializeObject(messageToprocess.payload);
                    AppointmentHistory appointmentHistory = JsonConvert.DeserializeObject<AppointmentHistory>(tempAppointmentHistory);

                    AppointmentHistory newAppointmentHistory = appointmentHistoryRepository.Add(appointmentHistory);
                    responseMessage = JsonConvert.SerializeObject(newAppointmentHistory);
                    break;

                case MessageEnums.MessageOperations.Delete:
                    appointmentHistoryRepository.Delete(Int32.Parse(idAppointment));
                    AppointmentHistory deletedAppointment = new() { Id = Int32.Parse(idAppointment) };
                    responseMessage = JsonConvert.SerializeObject(deletedAppointment);
                    break;

                case MessageEnums.MessageOperations.Get:
                    AppointmentHistory getAppointment = appointmentHistoryRepository.Get(Int32.Parse(idAppointment));
                    responseMessage = JsonConvert.SerializeObject(getAppointment);
                    break;

                //case MessageEnums.MessageOperations.GetAll:
                //    List<Appointment> Appointments = appointmentRepository.GetAll();
                //    responseMessage = JsonConvert.SerializeObject(Appointments);
                //    break;

                case MessageEnums.MessageOperations.Update:
                    var tempUpdateAppointment = JsonConvert.SerializeObject(messageToprocess.payload);
                    AppointmentHistory updateAppointment = JsonConvert.DeserializeObject<AppointmentHistory>(tempUpdateAppointment);

                    AppointmentHistory updated = appointmentHistoryRepository.Update(updateAppointment);
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
