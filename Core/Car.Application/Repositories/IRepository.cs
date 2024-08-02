using Car.Domain.Common;

namespace Car.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
    }
}
