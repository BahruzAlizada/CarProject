using Car.Domain.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car.Domain.Entities
{
    public class InteriorColor : BaseEntity
    {
        [NotMapped]
        public IFormFile? Photo { get; set;}
        public string Image { get; set; }
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
