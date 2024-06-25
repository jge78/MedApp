using Dapper;
using MEDApp.UserManagement.Api.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;
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

        public User Add(User user)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)");
            sb.Append($" VALUES({(int)user.UserType},'{user.FirstName}','{user.LastName}','{user.DateOfBirth.ToString("yyyyMMdd")}','{user.PhoneNumber}','{user.Email}');");
            sb.Append(" SELECT CAST(SCOPE_IDENTITY() AS int);");

            var id = db.Query<int>(sb.ToString(), user).Single();
            user.Id = id;
            return user;

        }

        public void Delete(int id)
        {
            this.db.Execute("DELETE FROM dbo.Users WHERE Id = @Id", new { id });
        }

        public List<User> GetAll()
        {
            return db.Query<User>("SELECT * FROM dbo.Users").ToList();
        }

        public User GetUser(int id)
        {
            return this.db.Query<User>("SELECT * FROM dbo.Users WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public User Update(User user)
        {

            var sql =
            "UPDATE  dbo.Users " +
            "SET FirstName = @FirstName," +
            " LastName = @LastName," +
            " DateOfBirth = @DateOfBirth," +
            " Email = @Email," +
            " PhoneNumber = @PhoneNumber" +
            " WHERE Id = @Id";

            this.db.Execute(sql, user);
            return user;
        }
    }
}
