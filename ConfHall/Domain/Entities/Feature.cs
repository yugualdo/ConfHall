namespace ConfHall.Domain.Entities
{
    using System;

    public class Feature : BaseEntity<Guid>
    {

        public Feature()
        {

        }

        public virtual string Name { get; set; }

    }
}