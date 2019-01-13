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

    public class HallService : IHallService
    {
        private IHallRepository _hallRepository;


        public HallService(IHallRepository hallRepository)
        {
            this._hallRepository = hallRepository;
        }


        /// <summary>
        /// Gets list all of all halls.
        /// </summary>
        /// <returns>Hall IEnumerable</returns>
        public IEnumerable<HallModel> Get()
        {
            IEnumerable<Hall> Hall = this._hallRepository.GetAll();
            return Hall.Select(c => Mapper.Map<HallModel>(c)).ToList();
        }

        public HallModel Get(Guid id)
        {
            var Hall = _hallRepository.Get(id);
            return Mapper.Map<HallModel>(Hall);
        }

        public Guid Add(HallModel model)
        {
            Hall hall = Mapper.Map<Hall>(model);
            string errors = Validate(hall);

            if (errors != null)
                throw new ValidationException(errors);

            _hallRepository.Insert(hall);
            return hall.Id;
        }

        public void Update(HallModel model)
        {
            Hall hall = Mapper.Map<Hall>(model);
            string errors = Validate(hall);

            if (errors != null)
                throw new ValidationException(errors);

            _hallRepository.Update(hall);
        }

        public void Delete(Guid id)
        {
            _hallRepository.Delete(id);
        }

        private string Validate(Hall hall)
        {
            List<string> errors = new List<string>();

            if (hall == null)
            {
                errors.Add("The Hall does not exist.");
            }
            if (errors.Any())
                return errors.Aggregate((c, n) => c + "*" + n);
            return null;
        }

    }
}
