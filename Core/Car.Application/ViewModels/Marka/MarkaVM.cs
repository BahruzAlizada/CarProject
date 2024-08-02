using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car.Application.ViewModels
{
    public class MarkaVM
    {
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public string Name { get; set; }
    }
}
