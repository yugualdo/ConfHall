namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Entities;
    using ConfHall.Domain.Repositories;
    using System;

    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
    }
}
