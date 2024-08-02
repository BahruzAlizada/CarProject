using Car.Domain.Common;

namespace Car.Domain.Entities
{
    public class CarPreference : BaseEntity
    {
        public Guid PreferenceId { get; set; }
        public Preference Preference { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
