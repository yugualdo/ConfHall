namespace ConfHall.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Feature : BaseEntity<Guid>
    {
        private ICollection<HallFeature> hallFeatures = new HashSet<HallFeature>();
        
        /// <summary>
        /// 
        /// </summary>
        public Feature()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerable<HallFeature> HallFeatures
        {
            get
            {
                return this.hallFeatures;
            }
        }
    }
}