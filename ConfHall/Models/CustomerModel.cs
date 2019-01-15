namespace ConfHall.Models
{
    using ConfHall.Domain.Model;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class CustomerModel : BaseModel<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IdNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Decimal Balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PhoneNumber { get; set; }
    }
}
