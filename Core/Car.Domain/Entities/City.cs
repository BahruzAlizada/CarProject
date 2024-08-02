using Car.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Car.Domain.Entities
{
    public class City : BaseEntity
    {
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
