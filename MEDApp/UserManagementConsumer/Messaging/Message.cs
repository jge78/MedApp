using static UserManagementConsumer.Messaging.MessageEnums;

namespace UserManagementConsumer.Messaging
{
    public class Message
    {
        public OperationTypes operationType { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; } 

    }
}
