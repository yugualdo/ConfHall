namespace ConfHall.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public User() { }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
    }
}
