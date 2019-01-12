namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Entities;
    using System;

    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
