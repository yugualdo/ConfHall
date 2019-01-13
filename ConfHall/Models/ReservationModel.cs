using ConfHall.Domain.Entities;
using ConfHall.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Models
{
    public class ReservationModel:BaseModel<Guid>
    {        
        public virtual Guid HallId { get; set; }      
        public virtual Guid CustomerId { get; set; }
        public virtual DateTime From { get; set; }
        public virtual DateTime To { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual bool IsPaid { get; set; }
        public virtual bool IsConfirmed { get; set; }
    }
}
