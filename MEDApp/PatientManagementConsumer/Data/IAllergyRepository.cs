using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    public interface IAllergyRepository
    {
        Allergy Get(int id);
        Allergy Add(Allergy allergy);
        Allergy Update(Allergy allergy);
        void Delete(int id);
    }
}
