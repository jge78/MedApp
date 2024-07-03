namespace AppointmentsConsumer.Data.Models
{
    public class Shift
    {
        public Int32 Id { get; set; }
        public Int32 Medic { get; set; }
        public Int32 DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
