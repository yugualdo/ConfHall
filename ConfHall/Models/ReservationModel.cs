using ConfHall.Domain.Entities;
using ConfHall.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ReservationModel : AuditableModel<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Guid HallId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Guid CustomerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime From { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime To { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Decimal Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsPaid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsConfirmed { get; set; }
    }
}
