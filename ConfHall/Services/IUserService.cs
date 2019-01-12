namespace ConfHall.Domain.Services
{
    using ConfHall.Model;
    using System;
    using System.Collections.Generic;

    public interface IUserService
    {
        #region Methods

        Guid Add(UserModel user);

        IEnumerable<UserModel> Get();

        UserModel Get(Guid id);

        void Update(UserModel user);

        UserModel GetCurrentUser();

        #endregion Methods
    }
}
