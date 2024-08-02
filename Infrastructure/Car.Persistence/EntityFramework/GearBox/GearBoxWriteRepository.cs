using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.Repositories;

namespace Car.Persistence.EntityFramework
{
    public class GearBoxWriteRepository : WriteRepository<GearBox>,IGearBoxWriteRepository
    {
    }
}
