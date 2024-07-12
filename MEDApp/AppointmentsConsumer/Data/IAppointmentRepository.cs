using AppointmentsConsumer.Data.Models;

namespace AppointmentsConsumer.Data
{
    public interface IAppointmentRepository
    {
        bool InitializeDB();
        Appointment Get(int id);
        List<Appointment> GetAll();
        Appointment Add(Appointment appointment);
        Appointment Update(Appointment appointment);
        void Delete(int id);
    }
}
