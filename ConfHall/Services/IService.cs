using System.Collections.Generic;

namespace ConfHall.Domain.Services
{
    public interface IService<TModel, TKey>
    {
        #region Methods

        /// <summary>
        /// Gets Element by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>TModel Element</returns>
        TModel Get(TKey id);

        /// <summary>
        /// Gets All elements TModel.
        /// </summary>
        /// <returns>IEnumerable<TModel></returns>
        IEnumerable<TModel> Get();

        /// <summary>
        /// Added TModel Element.
        /// </summary>
        /// <param name="model">The TModel.</param>
        /// <returns>TKey value</returns>
        TKey Add(TModel model);

        /// <summary>
        /// Update a Element.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(TModel model);

        /// <summary>
        /// Delete a Element by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Tkey Value</returns>
        void Delete(TKey id);

        #endregion
    }
}
