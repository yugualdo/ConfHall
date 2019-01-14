namespace ConfHall.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class Customer : BaseEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public Customer()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]{9,10}$", ErrorMessage = "Only accept 9-10 lenght numeric characters")]
        public virtual string IdNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Decimal Balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Only 11 lenght numeric characters")]
        public virtual string PhoneNumber { get; set; }
    }
}
