using ConfHall.Domain.Data;
using ConfHall.Domain.Entities;
using ConfHall.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConfHall.Domain.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConfHallDBContext _context;
        private DbSet<Customer> _entities;
        private string _errorMessage = string.Empty;

        public CustomerRepository(ConfHallDBContext context)
        {
            _context = context;
            _entities = context.Set<Customer>();
        }

        public IQueryable<Customer> GetAll()
        {
            try
            {
                return _entities.AsQueryable<Customer>().AsNoTracking();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Customer>().AsQueryable();
            }
        }

        public Customer Get(Guid id)
        {
            try
            {
                return _entities.Where(p => p.Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Guid Insert(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                _entities.Add(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return Guid.Empty;
            }
        }

        public Guid Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                Customer entity = _entities.Where(p => p.Id.Equals(Id)).SingleOrDefault();
                _context.Remove(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public Guid Update(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _entities.Update(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}
