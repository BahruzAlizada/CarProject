using Car.Application.Repositories;
using Car.Application.ViewModels;
using Car.Domain.Entities;

namespace Car.Application.Abstract
{
    public interface IModelReadRepository : IReadRepository<Category>
    {
        Task<List<ModelListVM>> GetModelsWithPageAsync(string name, Guid? parentId, int take, int page);
        Task<double> GetModelsPageCountAsync(double take);
    }
}
