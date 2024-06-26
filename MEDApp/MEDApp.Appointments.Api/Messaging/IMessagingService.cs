using MEDApp.Appointments.Api.Models;

namespace MEDApp.Appointments.Api.Messaging
{
    public interface IMessagingService
    {
        public string Send<T>(T message);
        public string AddAppointment<T>(T message);
        public string DeleteAppointment(int id);
        public Appointment GetAppointment(int id);
        public List<Appointment> GetAllAppointment();
        public Appointment UpdateAppointment<T>(T message);

    }
}
