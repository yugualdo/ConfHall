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
    public class CustomerService : ICustomerService
    {
        private IReservationRepository _reservationRepository;
        private IHallRepository _hallRepository;
        private ICustomerRepository _customerRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="reservationRepository"></param>
        /// <param name="hallRepository"></param>
        public CustomerService(ICustomerRepository customerRepository, IReservationRepository reservationRepository, IHallRepository hallRepository)
        {
            _customerRepository = customerRepository;
            _reservationRepository = reservationRepository;
            _hallRepository = hallRepository;
        }


        /// <summary>
        /// Gets list all of all customers.
        /// </summary>
        /// <returns>Hall IEnumerable</returns>
        public IEnumerable<CustomerModel> Get()
        {
            IEnumerable<Customer> Customer = this._customerRepository.GetAll();
            return Customer.Select(c => Mapper.Map<CustomerModel>(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerModel Get(Guid id)
        {
            var Customer = _customerRepository.Get(id);
            return Mapper.Map<CustomerModel>(Customer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid Add(CustomerModel model)
        {
            Customer customer = Mapper.Map<Customer>(model);
            string errors = Validate(customer);

            if (errors != null)
                throw new ValidationException(errors);

            _customerRepository.Insert(customer);
            return customer.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Update(CustomerModel model)
        {
            Customer customer = Mapper.Map<Customer>(model);
            string errors = Validate(customer);

            if (errors != null)
                throw new ValidationException(errors);

            _customerRepository.Update(customer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            

            if (!(_reservationRepository.GetAll().Where(r => r.IsConfirmed == false && r.Customer.Id == id).Any()))
            {
                _customerRepository.Delete(id);
            }
            else {
                throw new ValidationException("Can't delete a customer with unconfirmed reservations.");
            }
        }

        private string Validate(Customer customer)
        {
            List<string> errors = new List<string>();

            if (customer == null)
            {
                errors.Add("The Customer does not exist.");
            }
            if (errors.Any())
                return errors.Aggregate((c, n) => c + "*" + n);
            return null;
        }

    }
}
