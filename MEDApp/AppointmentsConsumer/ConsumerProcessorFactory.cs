using AppointmentsConsumer.Data;

namespace AppointmentsConsumer
{
    public class ConsumerProcessorFactory
    {
        public static ConsumerProcessor Create(string messageEntity, string dataConectionString)
        {
            switch (messageEntity)
            {
                case "MEDApp.Appointments.Api.Models.Appointment":
                    return new AppointmentConsumerProcessor(dataConectionString);
                case "MEDApp.Appointments.Api.Models.Shift":
                    return new ShiftConsumerProcessor(dataConectionString);
                default:
                    //TODO:Implementar NULL
                    return new AppointmentConsumerProcessor(dataConectionString);
                    break;
            }
        }

    }
}
