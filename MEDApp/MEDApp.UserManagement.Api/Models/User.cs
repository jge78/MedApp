namespace MEDApp.UserManagement.Api.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        public Enums.UserTypes UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
