namespace ConfHall.Domain.Model
{
    using ConfHall.Enums;

    public class FilterModel
    {
        public string PropertyName { get; set; }
        public Operator Operation { get; set; }
        public object Value { get; set; }
    }
}
