using static AppointmentsConsumer.Messaging.MessageEnums;

namespace AppointmentsConsumer.Messaging
{
    public class Message
    {
        public string messageEntity { get; set; }
        public MessageOperations messageOperation { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; } 

    }
}
