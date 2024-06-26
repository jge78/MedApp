namespace AppointmentsConsumer.Data.Models
{
    public class Appointment
    {
        public Int32 Id { get; set; }
        public Int32 Patient { get; set; }
        public Int32 Medic { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
