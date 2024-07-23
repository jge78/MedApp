using Dapper;
using MedAppJwtAuthenticationManager.Models;
using System.Data;
using System.Data.SqlClient;

namespace MedAppJwtAuthenticationManager.Data
{
    internal class AccountRepository : IAccountRepository
    {
        private IDbConnection db;

        public AccountRepository(string connectionString)
        {
            db = new SqlConnection(connectionString);
        }
        public List<UserAccount> GetAllAccounts()
        {
            return db.Query<UserAccount>("SELECT Id,Email AS UserName,Password,CASE UserType WHEN 0 THEN 'Admin' WHEN 1 THEN 'Medic' WHEN 2 THEN 'User' WHEN 3 THEN 'Patient' END AS Role FROM dbo.Users ").ToList();
        }
        public UserAccount GetAccount(string email)
        {
            return db.Query<UserAccount>("SELECT Id,Email AS UserName,Password,CASE UserType WHEN 0 THEN 'Admin' WHEN 1 THEN 'Medic' WHEN 2 THEN 'User' WHEN 3 THEN 'Patient' END AS Role FROM dbo.Users WHERE Email = @Email", new { email }).SingleOrDefault();
        }

    }
}
