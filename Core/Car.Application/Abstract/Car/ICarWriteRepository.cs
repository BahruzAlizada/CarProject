using Car.Application.Repositories;

namespace Car.Application.Abstract
{
    public interface ICarWriteRepository : IWriteRepository<Car.Domain.Entities.Car>
    {
    }
}
