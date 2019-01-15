namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Data;
    using ConfHall.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class HallRepository : IHallRepository
    {
        private readonly ConfHallDBContext _context;
        private DbSet<Hall> _entities;
        private DbSet<HallFeature> _hallFeatures;
        private DbSet<Feature> _features;
        private string _errorMessage = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public HallRepository(ConfHallDBContext context)
        {
            _context = context;
            _entities = context.Set<Hall>();
            _hallFeatures = context.Set<HallFeature>();
            _features = context.Set<Feature>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Hall Get(Guid id)
        {
            try
            {
                Hall hall = _entities.Where(p => p.Id.Equals(id)).FirstOrDefault();
                return hall;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Feature> GetFeatures(Guid id)
        {
            try
            {
                return _hallFeatures.Where(hf => hf.Hall.Id == id).Select(hf => hf.Feature).ToList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IQueryable<Feature> GetFeatures(Guid id)
        //{
        //    return _features.AsQueryable<Feature>().Where(f => f.Hall.Id == id);

        //}
    }
}
