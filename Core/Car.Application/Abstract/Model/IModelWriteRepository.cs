using Car.Application.Repositories;
using Car.Domain.Entities;

namespace Car.Application.Abstract.Model
{
    public interface IModelWriteRepository : IWriteRepository<Category>
    {
    }
}
