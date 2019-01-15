namespace ConfHall.Domain.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class BaseModel<TKey>
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public BaseModel() { }

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; set; }

        #endregion
    }
}
