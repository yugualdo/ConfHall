namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Entities;
    using System;

    public interface IReservationRepository : IRepository<Reservation, Guid>
    {
    }
}
