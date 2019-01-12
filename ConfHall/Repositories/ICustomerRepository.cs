namespace ConfHall.Repositories
{
    using ConfHall.Domain.Entities;
    using ConfHall.Domain.Repositories;
    using System;

    interface ICustomerRepository : IRepository<Customer, Guid>
    {
    }
}
