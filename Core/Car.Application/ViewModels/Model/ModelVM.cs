using System.ComponentModel.DataAnnotations;

namespace Car.Application.ViewModels
{
    public class ModelVM
    {
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public string Name { get; set; }
        public Guid ParentId { get; set; }
    }
}
