using AppointmentsConsumer.Data;
using AppointmentsConsumer.Messaging;

namespace AppointmentsConsumer
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
