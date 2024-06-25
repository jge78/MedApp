namespace MEDApp.UserManagement.Api.Messaging
{
    public interface IMessagingService
    {
        public void SendMessage<T>(T message);

    }
}
