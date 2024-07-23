using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    public interface IAppointmentHistoryRepository
    {
        AppointmentHistory Get(int id);
        AppointmentHistory Add(AppointmentHistory appointmentHistory);
        AppointmentHistory Update(AppointmentHistory appointmentHistory);
        void Delete(int id);
    }
}
