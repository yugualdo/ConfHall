namespace ConfHall.Domain.Model
{
    using ConfHall.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Operator Operation { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
    }
}
