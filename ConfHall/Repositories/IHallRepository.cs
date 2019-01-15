namespace ConfHall.Domain.Repositories
{
    using ConfHall.Domain.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IHallRepository : IRepository<Hall, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Feature> GetFeatures(Guid id);
    }
}
