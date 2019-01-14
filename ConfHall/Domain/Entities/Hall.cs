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
        private ICollection<HallFeature> _hallFeatures = new HashSet<HallFeature>();

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
        public virtual IEnumerable<HallFeature> HallFeatures
        {
            get
            {
                return _hallFeatures;
            }
        }


        //public virtual void AddFeature(Feature feature)
        //{
        //    HallFeature hallfeature = new HallFeature();
        //    hallfeature.Feature = feature;
        //    hallfeature.FeatureId = feature.Id;
        //    hallfeature.Hall = this;
        //    hallfeature.HallId = this.Id;
        //    if (!_hallFeatures.Contains(hallfeature))
        //    {
        //        _hallFeatures.Add(hallfeature);
        //    }
        //}

        //public virtual void RemoveFeature(Feature hallFeature)
        //{

        //}
    }
}
