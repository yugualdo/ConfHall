namespace ConfHall.Services
{
    using ConfHall.Domain.Services;
    using ConfHall.Models;
    using System;

    public interface ICustomerService : IService<CustomerModel, Guid>
    {

    }
}
