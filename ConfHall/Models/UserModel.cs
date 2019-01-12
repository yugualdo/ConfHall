namespace ConfHall.Model
{
    using System;

    public class UserModel
    {
        public UserModel() { }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

    }
}
