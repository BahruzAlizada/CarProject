using Car.Application.Repositories;
using Car.Domain.Entities;

namespace Car.Application.Abstract
{
    public interface IStatusReadRepository : IReadRepository<Status>
    {
    }
}
