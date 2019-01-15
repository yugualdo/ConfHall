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
    public class HallService : IHallService
    {
        private IHallRepository _hallRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hallRepository"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HallModel Get(Guid id)
        {
            Hall hall = _hallRepository.Get(id);
            HallModel model = Mapper.Map<HallModel>(hall);
            model.Features = _hallRepository.GetFeatures(hall.Id).Select(f=>Mapper.Map<FeatureModel>(f)).ToList();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid Add(HallModel model)
        {
            Hall hall = Mapper.Map<Hall>(model);
            string errors = Validate(hall);

            if (errors != null)
                throw new ValidationException(errors);

            _hallRepository.Insert(hall);
            return hall.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Update(HallModel model)
        {
            Hall hall = Mapper.Map<Hall>(model);
            string errors = Validate(hall);

            if (errors != null)
                throw new ValidationException(errors);

            _hallRepository.Update(hall);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            _hallRepository.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hall"></param>
        /// <returns></returns>
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
