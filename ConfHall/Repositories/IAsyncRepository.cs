namespace ConfHall.Domain.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IAsyncRepository<TEntity, TKey>
    {
        #region Methods

        /// <summary>
        /// Gets Element by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>TEntity Element</returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Gets All elements TEntity.
        /// </summary>
        /// <returns>IEnumerable<TEntity></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get all notifications.
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>Notification list</returns>
        IQueryable<TEntity> GetAll(int pageSize, int pageIndex);

        /// <summary>
        /// Added TEntity Element.
        /// </summary>
        /// <param name="entity">The TEntity.</param>
        /// <returns>Int value</returns>
        Task<TKey> Insert(TEntity entity);

        /// <summary>
        /// Update a Element.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>int value</returns>
        Task<TKey> Update(TEntity entity);

        /// <summary>
        /// Delete a Element by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Int Value</returns>
        Task<TKey> Delete(TKey id);

        #endregion
    }
}
