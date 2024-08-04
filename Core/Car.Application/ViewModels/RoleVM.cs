using System.ComponentModel.DataAnnotations;

namespace Car.Application.ViewModels
{
    public class RoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
