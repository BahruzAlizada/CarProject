using Car.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Car.Domain.Entities
{
    public class Transmitter : BaseEntity // Ötrücü
    {
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
