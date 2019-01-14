namespace ConfHall.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class Reservation : AuditableEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public Reservation() { }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual Hall Hall { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual Customer Customer { get; set; }
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
        public virtual decimal Price { get; set; }
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
