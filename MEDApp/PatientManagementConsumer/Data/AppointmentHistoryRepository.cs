using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    internal class AppointmentHistoryRepository : IAppointmentHistoryRepository
    {
        public AppointmentHistory Add(AppointmentHistory appointmentHistory)
        {
            return new AppointmentHistory
            {
                Id = 99,
                Medic = 1,
                Patient = 1,
                Date = DateTime.Now,
                Time = DateTime.Now,
                RequieresNewAppointment = false,
                TreatmentInstructions = new List<string> { "Take care 1","Take care 2","Take care 3" }
            };
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AppointmentHistory Get(int id)
        {
            return new AppointmentHistory
            {
                Id = 1,
                Medic = 1,
                Patient = 1,
                Date = DateTime.Now,
                Time = DateTime.Now,
                RequieresNewAppointment = false,
                TreatmentInstructions = new List<string> { "Take care 1", "Take car 2" }
            };
        }

        public AppointmentHistory Update(AppointmentHistory appointmentHistory)
        {
            //throw new NotImplementedException();
            return new AppointmentHistory
            {
                Id = 1,
                Medic = 1,
                Patient = 1,
                Date = DateTime.Now,
                Time = DateTime.Now,
                RequieresNewAppointment = false,
                TreatmentInstructions = new List<string> { "Take care 1", "Take car 2", "take care 3" }
            };

        }
    }
}
