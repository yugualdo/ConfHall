namespace ConfHall.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Customer : BaseEntity<Guid>
    {
        public Customer()
        {
        }

        [Required]
        public virtual string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{9,10}$", ErrorMessage = "Only accept 9-10 lenght numeric characters")]
        public virtual string IdNumber { get; set; }
        public virtual Decimal Balance { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Only 11 lenght numeric characters")]
        public virtual string PhoneNumber { get; set; }
    }
}
