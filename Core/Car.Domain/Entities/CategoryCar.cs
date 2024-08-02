using Car.Domain.Common;

namespace Car.Domain.Entities
{
    public class CategoryCar : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
