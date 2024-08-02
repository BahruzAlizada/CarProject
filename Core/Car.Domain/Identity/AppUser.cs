using Car.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Car.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? Image { get; set; }
        public string Name { get;set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int? Seen { get; set; }
        public bool? IsCompany { get; set; }
        public string UserRole { get; set; }

        public ICollection<Entities.Car> Cars { get; set; }

    }
}
