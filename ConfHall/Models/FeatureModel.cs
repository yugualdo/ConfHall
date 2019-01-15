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
    public class FeatureModel : BaseModel<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
    }
}
