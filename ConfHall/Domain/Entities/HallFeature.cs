using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Domain.Entities
{
    public class HallFeature
    {
        public Guid HallId { get; set; }
        public Guid FeatureId { get; set; }
        public Hall Hall { get; set; }
        public Feature Feature { get; set; }
    }
}
