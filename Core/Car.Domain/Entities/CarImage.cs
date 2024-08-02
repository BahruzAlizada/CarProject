using Car.Domain.Common;

namespace Car.Domain.Entities
{
    public class CarImage : BaseEntity
    {
        public string Image { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
