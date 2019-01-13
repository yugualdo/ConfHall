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

    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;


        public ReservationService(IReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }


        /// <summary>
        /// Gets list all of all reservations.
        /// </summary>
        /// <returns>Reservation IEnumerable</returns>
        public IEnumerable<ReservationModel> Get()
        {
            IEnumerable<Reservation> Reservation = this._reservationRepository.GetAll();
            return Reservation.Select(c => Mapper.Map<ReservationModel>(c)).ToList();
        }

        public ReservationModel Get(Guid id)
        {
            var Reservation = _reservationRepository.Get(id);
            return Mapper.Map<ReservationModel>(Reservation);
        }

        public Guid Add(ReservationModel model)
        {
            Reservation reservation = Mapper.Map<Reservation>(model);
            string errors = Validate(reservation);

            if (errors != null)
                throw new ValidationException(errors);

            _reservationRepository.Insert(reservation);
            return reservation.Id;
        }

        public void Update(ReservationModel model)
        {
            Reservation reservation = Mapper.Map<Reservation>(model);
            string errors = Validate(reservation);

            if (errors != null)
                throw new ValidationException(errors);

            _reservationRepository.Update(reservation);
        }

        public void Delete(Guid id)
        {
            _reservationRepository.Delete(id);
        }

        private string Validate(Reservation hall)
        {
            List<string> errors = new List<string>();

            if (hall == null)
            {
                errors.Add("The Reservation does not exist.");
            }
            if (errors.Any())
                return errors.Aggregate((c, n) => c + "*" + n);
            return null;
        }

    }
}
