
using Car.Application.Repositories;
using Car.Application.ViewModels;
using Car.Domain.Entities;

namespace Car.Application.Abstract
{
    public interface IMarkaReadRepository : IReadRepository<Category>
    {
        Task<List<MarkaListVM>> GetMarkasWithPageAsync(string search, int take, int page);
        Task<double> GetMarkasPageCountAsync(double take);
    }
}
