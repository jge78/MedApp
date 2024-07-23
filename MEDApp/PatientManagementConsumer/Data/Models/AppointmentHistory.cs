namespace PatientManagementConsumer.Data.Models
{
    public class AppointmentHistory
    {
        public Int32 Id { get; set; }
        public Int32 Patient { get; set; }
        public Int32 Medic { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool RequieresNewAppointment { get; set; }
        public List<string>? TreatmentInstructions { get; set; }
    }
}
