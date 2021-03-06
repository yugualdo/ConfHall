﻿namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Data;
    using ConfHall.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ConfHallDBContext context;


        private DbSet<User> entities;
        string errorMessage = string.Empty;
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ConfHallDBContext context)
        {
            this.context = context;
            this.entities = context.Set<User>();
        }
        #endregion Constructors

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
                User entity = this.entities.Where(p => p.Id.Equals(Id)).SingleOrDefault();
                entity.IsActive = false;
                this.context.Update(entity);
                this.context.SaveChanges();
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
        public User Get(Guid id)
        {
            try
            {
                return this.entities.Where(p => p.Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAll()
        {
            try
            {
                return this.entities.AsQueryable<User>().Where(o => o.IsActive == true).AsNoTracking();
            }
            catch (Exception)
            {
                return Enumerable.Empty<User>().AsQueryable();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Guid Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                this.entities.Add(entity);
                this.context.SaveChanges();
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
        public Guid Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                this.entities.Update(entity);
                this.context.SaveChanges();

                return entity.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}