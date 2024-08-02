using Car.Domain.Common;
using Car.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using static Car.Domain.DomainHelper.Enums;

namespace Car.Domain.Entities
{
    public class Car : BaseEntity
    {
        public string? VIN { get; set; }
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public int EnginePower { get; set; }
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public double Price { get; set; }
        public Currency Currency { get; set; }
        [Required(ErrorMessage = "Bu xan boş ola bilməz")]
        public int Probeq { get; set; }
        public ProbeqType ProbeqType { get; set; }

        public string? Description { get; set; }

        public bool ItIsColored { get; set; }
        public bool ItIsHit { get; set; }

        public bool HasCredit { get; set; }
        public bool HasBarter { get; set; }


        public bool IsNew { get; set; }
        public DateTime? Updated { get; set; }
        public int Seen { get; set; }
        public bool IsPremium { get; set; }


        public ICollection<CarImage> Images { get; set; }
        public ICollection<CategoryCar> CategoryCars { get; set; }
        public ICollection<CarPreference> CarPreferences { get; set; }


        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid BanId { get; set; }
        public Ban Ban { get; set; }


        public Guid YearId { get; set; }
        public Year Year { get; set; }


        public Guid GearBoxId { get; set; }
        public GearBox GearBox { get; set; }


        public Guid EngineTypeId { get; set; }
        public EngineType EngineType { get; set; }


        public Guid EngineCapacityId { get; set; }
        public EngineCapacity EngineCapacity { get; set; }


        public Guid TransmitterId { get; set; }
        public Transmitter Transmitter { get; set; }


        public Guid ExteriorColorId { get; set; }
        public ExteriorColor ExteriorColor { get; set; }


        public Guid InteriorColorId { get; set; }
        public InteriorColor InteriorColor { get; set; }


        public Guid SeatId { get; set; }
        public Seat Seat { get; set; }


        public Guid OwnerId { get; set; } 
        public Owner Owner { get; set; }


        public Guid CityId { get; set; }
        public City City { get; set; }


        public Guid StatusId { get; set; }
        public Status Status { get; set; }
    }
}
