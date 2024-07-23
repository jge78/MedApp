namespace MEDApp.PatientManagement.Api.Models
{
    public class Study
    {
        public Int32 Id { get; set; }
        public Int32 Patient { get; set; }
        public string StudyName { get; set; }
        public DateTime Date { get; set; }
        public string FilePath { get; set; }
    }
}
