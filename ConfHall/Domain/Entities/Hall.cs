namespace ConfHall.Domain.Entities
{
    using ConfHall.Enums;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Hall : BaseEntity<Guid>
    {
        private ICollection<Feature> _features = new HashSet<Feature>();

        /// <summary>
        /// 
        /// </summary>
        public Hall()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual HallType HallType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Decimal PricePerHour { get; set; }


        
    }
}
