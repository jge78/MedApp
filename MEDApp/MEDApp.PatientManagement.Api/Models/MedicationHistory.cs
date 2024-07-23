namespace MEDApp.PatientManagement.Api.Models
{
    public class MedicationHistory
    {
        public Int32 Id { get; set; }
        public Int32 Patient { get; set; }
        public string? MedicationDetails { get; set; }
        public DateTime Date { get; set; }
        public bool CurrentlyUsing {  get; set; }
    }
}
