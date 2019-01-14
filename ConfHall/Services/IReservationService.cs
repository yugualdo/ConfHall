namespace ConfHall.Services
{
    using ConfHall.Domain.Services;
    using ConfHall.Models;
    using System;
    using System.Collections.Generic;

    public interface IReservationService : IService<ReservationModel, Guid>
    {
        IEnumerable<ReservationModel> Top(Guid customerId);
        IEnumerable<ReservationModel> GetUnconfirmed();
        IEnumerable<ReservationModel> GetPendingPayment();
        void Confirm(Guid id);
        void Pay(Guid id);
    }
}
