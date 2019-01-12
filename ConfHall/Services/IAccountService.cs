namespace ConfHall.Domain.Services
{
    using ConfHall.Model;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task<AccountModel> PasswordSignInAsync(AccountModel model, string remoteIpAddreess);
        void Logout();
    }
}
