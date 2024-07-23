using MedAppJwtAuthenticationManager.Models;

namespace MedAppJwtAuthenticationManager.Data
{
    public interface IAccountRepository
    {
        List<UserAccount> GetAllAccounts();
        UserAccount GetAccount(String userName);
    }
}
