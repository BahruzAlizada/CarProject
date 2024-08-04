using Car.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Car.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Image { get; set; }
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public bool IsMain { get; set; }
        public bool? Collection { get; set; }

        public ICollection<CategoryCar> CategoryCars { get; set; }
    }
}
