namespace UserManagementConsumer.Messaging
{
    public interface IConsumerService
    {
        public void ConsumeMessage<T>(T message);
    }
}
