﻿namespace ConfHall.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Feature : BaseEntity<Guid>
    {       
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
        
    }
}