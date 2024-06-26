using static MEDApp.UserManagement.Api.Messaging.MessageEnums;

namespace MEDApp.UserManagement.Api.Messaging
{
    public class Message
    {
        public OperationTypes operationType { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; } 

    }
}
