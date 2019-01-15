namespace ConfHall.Models
{
    using ConfHall.Domain.Entities;
    using ConfHall.Domain.Model;
    using ConfHall.Enums;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// 
    /// </summary>
    public class HallModel : BaseModel<Guid>
    {
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

        /// <summary>
        /// 
        /// </summary>
        public virtual List<FeatureModel> Features { get; set; }
    }
}
