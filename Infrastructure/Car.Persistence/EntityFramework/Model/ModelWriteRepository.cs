using Car.Application.Abstract.Model;
using Car.Domain.Entities;
using Car.Persistence.Repositories;

namespace Car.Persistence.EntityFramework
{
    public class ModelWriteRepository : WriteRepository<Category>,IModelWriteRepository
    {
    }
}
