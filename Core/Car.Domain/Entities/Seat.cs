using Car.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Car.Domain.Entities
{
    public class Seat : BaseEntity
    {
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public int SeatNumber { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
