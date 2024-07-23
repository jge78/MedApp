namespace MEDApp.UserManagement.Api.Messaging
{
    public interface IMessagingService
    {
        public Task<T> Add<T>(T message);
        public Task<bool> Delete(int id);
        public Task<T> Get<T>(int id);
        public Task<List<T>> GetAll<T>();
        public Task<T> Update<T>(T message);
        public Task<string> Send<T>(T message);

    }
}
