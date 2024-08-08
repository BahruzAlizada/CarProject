using Car.Application.Repositories;

namespace Car.Application.Abstract
{
    public interface ICarReadRepository : IReadRepository<Car.Domain.Entities.Car>
    {
    }
}
