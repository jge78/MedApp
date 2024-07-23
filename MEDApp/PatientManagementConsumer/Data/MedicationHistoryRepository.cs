using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    internal class MedicationHistoryRepository : IMedicationHistoryRepository
    {
        public MedicationHistory Add(MedicationHistory medicationHistory)
        {
            //TODO: Remove mock
            var newMedicationHistory = new MedicationHistory
            {
                Id = 99,
                Patient = medicationHistory.Patient,
                MedicationDetails = medicationHistory.MedicationDetails,
                Date = medicationHistory.Date,
                CurrentlyUsing = medicationHistory.CurrentlyUsing
            };

            return newMedicationHistory;
        }

        public void Delete(int id)
        {
            //TODO: Implements
            //throw new NotImplementedException();
            return;
        }

        public MedicationHistory Get(int id)
        {
            //TODO: Remove mock
            var newMedicationHistory = new MedicationHistory
            {
                Id = id,
                Patient = 1,
                MedicationDetails = "Medication Details, Name, active substance, quantity, frecuency",
                Date = DateTime.Now.AddMonths(-2),
                CurrentlyUsing = false
            };

            return newMedicationHistory;
        }

        public MedicationHistory Update(MedicationHistory medicationHistory)
        {
            //TODO: Remove mock
            var newMedicationHistory = new MedicationHistory
            {
                Id = medicationHistory.Id,
                Patient = medicationHistory.Patient,
                MedicationDetails = medicationHistory.MedicationDetails,
                Date = medicationHistory.Date,
                CurrentlyUsing = true
            };

            return newMedicationHistory;
        }
    }
}
