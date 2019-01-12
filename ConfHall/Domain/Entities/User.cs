namespace ConfHall.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class User : IdentityUser<Guid>
    {
        public User() { }

        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
