using Car.Domain.Entities;
using Car.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Car.Persistence.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OK3QKVJ;Database=CarProject;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;Integrated Security=True;");
        }

        public DbSet<Ban> Bans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<EngineCapacity> EngineCapacities { get; set; }
        public DbSet<GearBox> GearBoxes { get; set; }
        public DbSet<Owner> Owners { get; set; } 
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Transmitter> Transmitters { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<InteriorColor> InteriorColors { get; set; }
        public DbSet<ExteriorColor> ExteriorColors { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Domain.Entities.Car> Cars { get; set; }
        public DbSet<CategoryCar> CategoryCars { get; set; }
        public DbSet<CarPreference> CarPreferences { get; set; }
        public DbSet<CarImage> CarImages { get; set; }

    }
}
