using Car.Application.Abstract;
using Car.Persistence.Repositories;

namespace Car.Persistence.EntityFramework
{
    public class CarWriteRepository : WriteRepository<Car.Domain.Entities.Car>, ICarWriteRepository
    {
    }
}
