namespace ConfHall.Domain.Model
{
    public class BaseModel<TKey>
    {
        #region Constructor

        public BaseModel() { }

        #endregion

        #region Properties

        public TKey Id { get; set; }

        #endregion
    }
}
