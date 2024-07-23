using Dapper;
using MEDApp.UserManagement.Api.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace UserManagementConsumer.Data
{
    internal class UserRepository : IUserRepository
    {
        private IDbConnection db;

        public UserRepository(string connectionString)
        {
            db= new SqlConnection(connectionString);
        }

        public async Task<User> Add(User user)
        {
            //validate unique Email
            if (await EmailExistsInDB(user.Email))
            {
                //throw new Exception("Email already exists in Users Database");
                return new User();
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email,Password)");
            sb.Append($" VALUES({(int)user.UserType},'{user.FirstName}','{user.LastName}','{user.DateOfBirth.ToString("yyyyMMdd")}','{user.PhoneNumber}','{user.Email}','{user.Password}');");
            sb.Append(" SELECT CAST(SCOPE_IDENTITY() AS int);");

            var id = await db.QueryAsync<int>(sb.ToString(), user);
            user.Id = id.Single();
            return user;

        }

        public async Task<bool> Delete(int id)
        {
            var result = await db.ExecuteAsync("DELETE FROM dbo.Users WHERE Id = @Id", new { id });

            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await db.QueryAsync<User>("SELECT * FROM dbo.Users");
            return users.ToList();
        }

        public async Task<User> GetUser(int id)
        {
            var queryResult = await db.QueryAsync<User>("SELECT * FROM dbo.Users WHERE Id = @Id", new { id });
            User user = queryResult.SingleOrDefault();

            if (user == null)
            {
                return new User();
            }

            return user;
        }

        public async Task<User> Update(User user)
        {
            var sql =
            "UPDATE dbo.Users " +
            "SET FirstName = @FirstName," +
            " LastName = @LastName," +
            " DateOfBirth = @DateOfBirth," +
            " PhoneNumber = @PhoneNumber," +
            " Email = @Email," +
            " Password = @Password" +
            " WHERE Id = @Id";

            var queryResult = await db.ExecuteAsync(sql, user);

            if (queryResult == 0)
            {
                return new User();
            }
            
            User updatedUser = await GetUser(user.Id);
            return updatedUser;
        }

        public async Task<bool> InitializeDB()
        {
            if (await DatabaseExists())
                return true;

            return await CreateDB();
        }

        private async Task<bool> DatabaseExists()
        {
            var sql = "SELECT COUNT(1) FROM sys.databases WHERE name = 'MedAppUserDB'";
            var queryResult = await db.QueryAsync<int>(sql);

            return queryResult.SingleOrDefault() != 0;
        }

        private async Task<bool> CreateDB()
        {
            var sql =
             "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MedAppUserDB')" +
             " BEGIN CREATE DATABASE MedAppUserDB END";

            try
            {
                await db.ExecuteAsync(sql);
            }
            catch (Exception)
            {
                throw;
            }

            return await CreateTables();

        }

        private async Task <bool> CreateTables()
        {
            var sql =
                "USE MedAppUserDB; " +
                "CREATE TABLE dbo.Users" +
                "([Id] INT NOT NULL PRIMARY KEY IDENTITY," +
                "UserType INT NOT NULL CONSTRAINT CK_UserType CHECK(UserType IN(0,1,2,3))," +
                "FirstName VARCHAR(100) NOT NULL," +
                "LastName VARCHAR(100) NOT NULL," +
                "DateOfBirth DATE," +
                "PhoneNumber VARCHAR(50)," +
                "Email VARCHAR(100) NOT NULL CONSTRAINT UQ_Email UNIQUE," +
                "Password VARCHAR(100) NOT NULL); ";
            
            try
            {
                var execResult = await db.ExecuteAsync(sql);
                return execResult !=  0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EmailExistsInDB(string email)
        {
            var queryResult = await db.QueryAsync<int>("SELECT COUNT(1) FROM dbo.Users WHERE Email=@Email", new { email });
            return queryResult.SingleOrDefault() != 0;

        }
    }
}
