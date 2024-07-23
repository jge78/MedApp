namespace MEDApp.PatientManagement.Api.Models
{
    public class Allergy
    {
        public Int32 Id { get; set; }
        public Int32 Patient { get; set; }
        public List<string>? AllergyList { get; set; }
    }
}
