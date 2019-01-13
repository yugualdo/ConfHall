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
    using ConfHall.Repositories;

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;


        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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

        public CustomerModel Get(Guid id)
        {
            var Customer = _customerRepository.Get(id);
            return Mapper.Map<CustomerModel>(Customer);
        }

        public Guid Add(CustomerModel model)
        {
            Customer customer = Mapper.Map<Customer>(model);
            string errors = Validate(customer);

            if (errors != null)
                throw new ValidationException(errors);

            _customerRepository.Insert(customer);
            return customer.Id;
        }

        public void Update(CustomerModel model)
        {
            Customer customer = Mapper.Map<Customer>(model);
            string errors = Validate(customer);

            if (errors != null)
                throw new ValidationException(errors);

            _customerRepository.Update(customer);
        }

        public void Delete(Guid id)
        {
            _customerRepository.Delete(id);
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
