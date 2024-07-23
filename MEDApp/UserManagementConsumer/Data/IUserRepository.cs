using MEDApp.UserManagement.Api.Models;

namespace UserManagementConsumer.Data
{
    public interface IUserRepository
    {
        Task<bool> InitializeDB();
        Task<User> GetUser(int id);
        Task<List<User>> GetAll();
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<bool> Delete(int id);
        Task<bool> EmailExistsInDB(string email);
    }
}
