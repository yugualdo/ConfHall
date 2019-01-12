namespace ConfHall.Domain.Entities
{
    using ConfHall.Enums;
    using System;
    using System.Collections.Generic;

    public class Hall : BaseEntity<Guid>
    {
        private ICollection<Feature> features = new HashSet<Feature>();

        public Hall()
        {

        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual HallType HallType { get; set; }

        public virtual IEnumerable<Feature> Features
        {
            get
            {
                return this.features;
            }
        }
        public virtual void AddFeature(Feature feature)
        {
            if (!this.features.Contains(feature))
            {
                this.features.Add(feature);
            }
        }

        public virtual void RemoveFeature(Feature feature)
        {
            if (this.features.Contains(feature))
            {
                this.features.Remove(feature);
            }
        }
    }
}
