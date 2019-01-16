namespace ConfHall.Domain.Services
{
    using System.ComponentModel.DataAnnotations;
    using ConfHall.Domain.Entities;
    using ConfHall.Domain.Repositories;
    using ConfHall.Models;
    using System;
    using AutoMapper;
    using ConfHall.Services;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;
        private IHallRepository _hallRepository;
        private ICustomerRepository _customerRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservationRepository"></param>
        /// <param name="hallRepository"></param>
        /// <param name="customerRepository"></param>
        public ReservationService(IReservationRepository reservationRepository, IHallRepository hallRepository, ICustomerRepository customerRepository)
        {
            _reservationRepository = reservationRepository;
            _hallRepository = hallRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReservationModel> Get()
        {
            IEnumerable<Reservation> Reservation = this._reservationRepository.GetAll();
            return Reservation.Select(c => Mapper.Map<ReservationModel>(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReservationModel Get(Guid id)
        {
            var Reservation = _reservationRepository.Get(id);
            return Mapper.Map<ReservationModel>(Reservation);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid Add(ReservationModel model)
        {
            Reservation reservation = Mapper.Map<Reservation>(model);
            reservation.Hall = _hallRepository.Get(model.HallId);
            reservation.Customer = _customerRepository.Get(model.CustomerId);

            string errors = Validate(reservation);

            if (errors != null)
                throw new ValidationException(errors);

            _reservationRepository.Insert(reservation);
            return reservation.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Update(ReservationModel model)
        {
            Reservation reservation = Mapper.Map<Reservation>(model);
            string errors = Validate(reservation);

            if (errors != null)
                throw new ValidationException(errors);

            _reservationRepository.Update(reservation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            Reservation reservation = _reservationRepository.Get(id);
            if (reservation == null)
            {
                throw new ValidationException("The Reservation does not exist.");
            }
            else if (reservation.IsPaid == true || reservation.IsConfirmed == true)
            {
                throw new ValidationException("Can't delete a paid or confirmed reservation.");
            }
            _reservationRepository.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<ReservationModel> Top(Guid customerId)
        {
            IEnumerable<Reservation> Reservation = this._reservationRepository.GetAll().Where(r => r.Customer.Id == customerId).OrderByDescending(c => c.CreatedAt).Take(10);
            return Reservation.Select(c => Mapper.Map<ReservationModel>(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReservationModel> GetUnconfirmed()
        {
            IEnumerable<Reservation> Reservation = this._reservationRepository.GetAll().Where(r => r.IsConfirmed == false);
            return Reservation.Select(c => Mapper.Map<ReservationModel>(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReservationModel> GetPendingPayment()
        {
            IEnumerable<Reservation> Reservation = this._reservationRepository.GetAll().Where(r => r.IsPaid == false);
            return Reservation.Select(c => Mapper.Map<ReservationModel>(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Confirm(Guid id)
        {
            Reservation reservation = _reservationRepository.Get(id);
            if (reservation == null)
            {
                throw new ValidationException("The Reservation does not exist.");
            }
            else
            {
                reservation.IsConfirmed = true;
            }
            _reservationRepository.Update(reservation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Pay(Guid id)
        {
            Reservation reservation = _reservationRepository.Get(id);
            if (reservation == null)
            {
                throw new ValidationException("The Reservation does not exist.");
            }
            else
            {
                reservation.IsPaid = true;
            }
            _reservationRepository.Update(reservation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private string Validate(Reservation reservation)
        {
            List<string> errors = new List<string>();

            if (reservation == null)
            {
                errors.Add("The Reservation does not exist.");
            }
            if (_reservationRepository.GetAll().Where(r => r.Hall.Id == reservation.Hall.Id && (r.From > reservation.From && r.From < reservation.To || r.To < reservation.To && r.To > reservation.From)).Any())
            {
                errors.Add("Hall is reserved for that time lapse");
            }
            if (reservation.From.TimeOfDay.CompareTo(Convert.ToDateTime("10:00 PM").TimeOfDay) == 1 || reservation.From.TimeOfDay.CompareTo(Convert.ToDateTime("7:00 AM").TimeOfDay) == -1)
            {
                errors.Add("A reservation must begin after 7:00 AM and before 10:00 PM.");
            }
            if (reservation.From > reservation.To)
            {
                errors.Add("Reservation only must end after begin time");
            }
            if (errors.Any())
                return errors.Aggregate((c, n) => c + "*" + n);
            return null;
        }

    }
}
