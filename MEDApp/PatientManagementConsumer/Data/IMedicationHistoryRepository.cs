using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    public interface IMedicationHistoryRepository
    {
        MedicationHistory Get(int id);
        MedicationHistory Add(MedicationHistory medicationHistory);
        MedicationHistory Update(MedicationHistory medicationHistory);
        void Delete(int id);
    }
}
