namespace ConfHall.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class Feature : BaseEntity<Guid>
    {
        private ICollection<HallFeature> hallFeatures = new HashSet<HallFeature>();

        public Feature()
        {

        }

        public virtual string Name { get; set; }

        public virtual IEnumerable<HallFeature> HallFeatures
        {
            get
            {
                return this.hallFeatures;
            }
        }

    }
}