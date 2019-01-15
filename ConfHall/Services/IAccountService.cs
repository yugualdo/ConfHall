namespace ConfHall.Domain.Services
{
    using ConfHall.Model;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="remoteIpAddreess"></param>
        /// <returns></returns>
        Task<AccountModel> PasswordSignInAsync(AccountModel model, string remoteIpAddreess);
        /// <summary>
        /// 
        /// </summary>
        void Logout();
    }
}
