namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Data;
    using ConfHall.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConfHallDBContext _context;
        private DbSet<Customer> _entities;
        private string _errorMessage = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(ConfHallDBContext context)
        {
            _context = context;
            _entities = context.Set<Customer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
