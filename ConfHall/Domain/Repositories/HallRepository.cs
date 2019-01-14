using ConfHall.Domain.Data;
using ConfHall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Domain.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly ConfHallDBContext _context;
        private DbSet<Hall> _entities;
        private DbSet<HallFeature> _features;
        private string _errorMessage = string.Empty;

        public HallRepository(ConfHallDBContext context)
        {
            _context = context;
            _entities = context.Set<Hall>();
        }

        public IQueryable<Hall> GetAll()
        {
            try
            {
                return _entities.AsQueryable<Hall>().AsNoTracking();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hall>().AsQueryable();
            }
        }

        public Hall Get(Guid id)
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

        public Guid Insert(Hall entity)
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
                Hall entity = _entities.Where(p => p.Id.Equals(Id)).SingleOrDefault();
                _context.Remove(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public Guid Update(Hall entity)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<HallFeature> GetFeatures(Guid id)
        {
            return _features.AsQueryable<HallFeature>().Where(f => f.Hall.Id == id);

        }
    }
}
