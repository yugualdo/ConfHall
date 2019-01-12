namespace ConfHall.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class Role : IdentityRole<Guid>
    {
        public Role() { }

    }
}
