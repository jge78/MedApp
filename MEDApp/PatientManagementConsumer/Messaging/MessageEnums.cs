namespace PatientManagementConsumer.Messaging
{
    public class MessageEnums
    {
        public enum MessageTypes
        {
            AppointmentsHistory = 1
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
