namespace ConfHall.Services
{
    using ConfHall.Domain.Services;
    using ConfHall.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IReservationService : IService<ReservationModel, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IEnumerable<ReservationModel> Top(Guid customerId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReservationModel> GetUnconfirmed();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReservationModel> GetPendingPayment();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Confirm(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Pay(Guid id);
    }
}
