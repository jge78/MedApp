namespace MEDApp.Appointments.Api.Messaging
{
    public interface IMessagingService
    {
        public T Add<T>(T message);
        public T Delete<T>(int id);
        public T Get<T>(int id);
        public List<T> GetAll<T>();
        public T Update<T>(T message);
        public string Send<T>(T message);

    }
}
