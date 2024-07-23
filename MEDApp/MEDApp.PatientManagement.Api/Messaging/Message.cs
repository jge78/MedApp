using static MEDApp.PatientManagement.Api.Messaging.MessageEnums;

namespace MEDApp.PatientManagement.Api.Messaging
{
    public class Message
    {
        public string messageEntity { get; set; }
        public MessageOperations MessageOperation { get; set; }
        public string id { get; set; }
        public Object? payload { get; set; }
        //public OperationTypes operationType { get; set; }
        //public string id { get; set; }
        //public Object? payload { get; set; } 

    }
}
