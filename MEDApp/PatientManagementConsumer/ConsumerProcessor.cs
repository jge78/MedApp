using PatientManagementConsumer.Data;
using PatientManagementConsumer.Messaging;

namespace PatientManagementConsumer
{
    public abstract class ConsumerProcessor
    {
        private readonly string dataConectionString;

        public ConsumerProcessor(string dataConection)
        {
            dataConectionString = dataConection;
        }

        public abstract String ProcessMessage(Message messageToprocess);

    }
}
