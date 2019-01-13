namespace ConfHall.Domain.Entities
{
    using ConfHall.Enums;
    using System;
    using System.Collections.Generic;

    public class Hall : BaseEntity<Guid>
    {
        private ICollection<HallFeature> _hallFeatures = new HashSet<HallFeature>();

        public Hall()
        {

        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual HallType HallType { get; set; }

        public virtual IEnumerable<HallFeature> HallFeatures
        {
            get
            {
                return _hallFeatures;
            }
        }
        public virtual void AddFeature(Feature feature)
        {
            HallFeature hfeature = new HallFeature();
            hfeature.Feature = feature;
            hfeature.FeatureId = feature.Id;
            hfeature.Hall = this;
            hfeature.HallId = this.Id;
            if (!_hallFeatures.Contains(hfeature))
            {
                _hallFeatures.Add(hfeature);
            }
        }

        public virtual void RemoveFeature(Feature hallFeature)
        {
            
        }
    }
}
