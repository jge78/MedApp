using static PatientManagementConsumer.Messaging.MessageEnums;

namespace PatientManagementConsumer.Messaging
{
    public class Message
    {
        public string messageEntity { get; set; }
        public MessageOperations messageOperation { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; } 

    }
}
