using MEDApp.UserManagement.Api.Models;

namespace UserManagementConsumer.Data
{
    public interface IUserRepository
    {
        bool InitializeDB();
        User GetUser(int id);
        List<User> GetAll();
        User Add(User user);
        User Update(User user);
        void Delete(int id);
        bool EmailExistsInDB(string email);
    }
}
