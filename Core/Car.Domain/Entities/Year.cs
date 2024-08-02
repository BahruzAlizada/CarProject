using Car.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Car.Domain.Entities
{
    public class Year : BaseEntity
    {
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public int Yearr { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
