namespace ConfHall.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Reservation : BaseEntity<Guid>
    {
        public Reservation() { }

        [Required]
        public virtual Hall Hall { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }
        public virtual DateTime From { get; set; }
        public virtual DateTime To { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual bool IsPaid { get; set; }
        public virtual bool IsConfirmed { get; set; }
    }
}
