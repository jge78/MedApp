namespace MEDApp.UserManagement.Api.Messaging
{
    public interface IMessagingService
    {
        //public void SendMessage<T>(T message);
        public string SendMessage<T>(T message);

    }
}
