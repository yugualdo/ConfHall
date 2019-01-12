namespace ConfHall.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        #region Constructor

        public BaseEntity() { }

        #endregion

        #region Properties

        public TKey Id { get; set; }

        #endregion
    }
}
