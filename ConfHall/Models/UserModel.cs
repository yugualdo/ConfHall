namespace ConfHall.Model
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 
        /// </summary>
        public UserModel() { }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string PasswordHash { get; set; }

    }
}
