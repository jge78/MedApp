using static MEDApp.Appointments.Api.Messaging.MessageEnums;

namespace MEDApp.Appointments.Api.Messaging
{
    public class Message
    {
        public string messageEntity { get; set; }
        public MessageOperations MessageOperation { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; } 

    }
}
