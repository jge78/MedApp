using MEDApp.UserManagement.Api.Models;

namespace MEDApp.UserManagement.Api.Messaging
{
    public interface IMessagingService
    {
        public string Send<T>(T message);
        public string AddUser<T>(T message);
        public string DeleteUser(int id);
        public User GetUser(int id);
        public List<User> GetAllUsers();
        public User UpdateUser<T>(T message);

    }
}
