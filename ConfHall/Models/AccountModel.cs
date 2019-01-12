namespace ConfHall.Model
{
    public class AccountModel
    {
        public AccountModel() { }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }
        public string Expiration { get; set; }
    }
}
