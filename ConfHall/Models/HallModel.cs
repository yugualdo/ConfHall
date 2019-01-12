namespace ConfHall.Models
{
    using ConfHall.Domain.Model;
    using ConfHall.Enums;
    using System;

    public class HallModel : BaseModel<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual HallType HallType { get; set; }
    }
}
