using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    internal class AllergyRepository : IAllergyRepository
    {
        public Allergy Add(Allergy allergy)
        {
            //TODO: Remove mock
            var newAllergyRecord = new Allergy
            {
                Id = 99,
                Patient = allergy.Patient,
                AllergyList = allergy.AllergyList
            };

            return newAllergyRecord;
        }

        public void Delete(int id)
        {
            //TODO: Implement
            //throw new NotImplementedException();
            return;
        }

        public Allergy Get(int id)
        {
            //TODO: Remove mock
            var mockAllergyRecord = new Allergy
            {
                Id = id,
                Patient = 2,
                AllergyList = new List<string> {"Allergy to cat fur", "Allergy to penicillin"}
            };

            return mockAllergyRecord;

        }

        public Allergy Update(Allergy allergy)
        {
            //TODO: Remove mock
            allergy.AllergyList.Add("New TEST allergy");
            var updatedAllergyRecord = new Allergy
            {
                Id = allergy.Id,
                Patient = allergy.Patient,
                AllergyList = allergy.AllergyList
            };

            return updatedAllergyRecord;
        }
    }
}
