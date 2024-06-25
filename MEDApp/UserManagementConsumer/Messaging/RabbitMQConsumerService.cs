using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementConsumer.Messaging
{
    internal class RabbitMQConsumerService : IConsumerService
    {
        public void ConsumeMessage<T>(T message)
        {
            
            //TODO: take the message and save it to DB


        }
    }
}
