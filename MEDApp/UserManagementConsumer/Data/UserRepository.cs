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

        public User Add(User user)
        {
            //validate unique Email
            if (EmailExistsInDB(user.Email))
            {
                //throw new Exception("Email already exists in Users Database");
                return new User();
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email,Password)");
            sb.Append($" VALUES({(int)user.UserType},'{user.FirstName}','{user.LastName}','{user.DateOfBirth.ToString("yyyyMMdd")}','{user.PhoneNumber}','{user.Email}','{user.Password}');");
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
            "UPDATE dbo.Users " +
            "SET FirstName = @FirstName," +
            " LastName = @LastName," +
            " DateOfBirth = @DateOfBirth," +
            " PhoneNumber = @PhoneNumber" +
            " Email = @Email," +
            " Password = @Password," +
            " WHERE Id = @Id";

            this.db.Execute(sql, user);
            return user;
        }

        public bool InitializeDB()
        {
            if (DatabaseExists())
                return true;

            return CreateDB();
        }

        private bool DatabaseExists()
        {
            var sql = "SELECT COUNT(1) FROM sys.databases WHERE name = 'MedAppUserDB'";
            return db.Query<int>(sql).SingleOrDefault() != 0;
        }

        private bool CreateDB()
        {
            var sql =
             "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MedAppUserDB')" +
             " BEGIN CREATE DATABASE MedAppUserDB END";

            try
            {
                db.Execute(sql);
            }
            catch (Exception)
            {
                throw;
            }

            return CreateTables();

        }

        private bool CreateTables()
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
                db.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EmailExistsInDB(string email)
        {
            //var sql = "SELECT COUNT(1) FROM dbo.Users WHERE Email=@Email";
            return db.Query<int>("SELECT COUNT(1) FROM dbo.Users WHERE Email=@Email", new { email }).SingleOrDefault() != 0;
        }
    }
}
