namespace ConfHall.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public AccountModel() { }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Expiration { get; set; }
    }
}
