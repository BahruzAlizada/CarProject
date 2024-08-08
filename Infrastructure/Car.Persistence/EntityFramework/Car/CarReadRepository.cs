using Car.Application.Abstract;
using Car.Persistence.Repositories;

namespace Car.Persistence.EntityFramework
{
    public class CarReadRepository : ReadRepository<Car.Domain.Entities.Car>,ICarReadRepository
    {
    }
}
