using static MEDApp.Appointments.Api.Messaging.MessageEnums;

namespace MEDApp.Appointments.Api.Messaging
{
    public class Message
    {
        public OperationTypes operationType { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; } 

    }
}
