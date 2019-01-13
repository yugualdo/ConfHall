using ConfHall.Domain.Data;
using ConfHall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Domain.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ConfHallDBContext _context;
        private DbSet<Reservation> _entities;
        private string _errorMessage = string.Empty;

        public ReservationRepository(ConfHallDBContext context)
        {
            _context = context;
            _entities = context.Set<Reservation>();
        }

        public IQueryable<Reservation> GetAll()
        {
            try
            {
                return _entities.AsQueryable<Reservation>().AsNoTracking();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Reservation>().AsQueryable();
            }
        }

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
