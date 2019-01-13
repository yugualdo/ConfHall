namespace ConfHall.Models
{
    using ConfHall.Domain.Model;
    using System;

    public class CustomerModel : BaseModel<Guid>
    {

        public virtual string Name { get; set; }
        public virtual string IdNumber { get; set; }
        public virtual Decimal Balance { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}
