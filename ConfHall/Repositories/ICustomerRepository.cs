namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Entities;
    using ConfHall.Domain.Repositories;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
    }
}
