namespace ConfHall.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class BaseEntity<TKey>
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public BaseEntity() { }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; set; }
        #endregion
    }
}
