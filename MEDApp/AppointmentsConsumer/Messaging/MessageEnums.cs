namespace AppointmentsConsumer.Messaging
{
    public class MessageEnums
    {
        public enum MessageTypes
        {
            Appointment = 1,
            Shift
        }

        public enum MessageOperations
        {
            Add = 1,
            Delete,
            Get,
            GetAll,
            Update
        }
    }
}
