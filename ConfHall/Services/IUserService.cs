namespace ConfHall.Domain.Services
{
    using ConfHall.Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Guid Add(UserModel user);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel Get(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        void Update(UserModel user);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        UserModel GetCurrentUser();
        #endregion Methods
    }
}
