using ConfHall.Domain.Data;
using ConfHall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ReservationRepository : IReservationRepository
    {
        private readonly ConfHallDBContext _context;
        private DbSet<Reservation> _entities;
        private string _errorMessage = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ReservationRepository(ConfHallDBContext context)
        {
            _context = context;
            _entities = context.Set<Reservation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Reservation> GetAll()
        {
            try
            {
                return _entities.AsQueryable<Reservation>().AsNoTracking().Include("Hall").Include("Customer");
            }
            catch (Exception)
            {
                return Enumerable.Empty<Reservation>().AsQueryable();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reservation Get(Guid id)
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
        public Guid Insert(Reservation entity)
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
                Reservation entity = _entities.Where(p => p.Id.Equals(Id)).SingleOrDefault();
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
        public Guid Update(Reservation entity)
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
