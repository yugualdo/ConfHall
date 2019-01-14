using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfHall.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class HallFeature : BaseEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Hall Hall { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Feature Feature { get; set; }
    }
}
