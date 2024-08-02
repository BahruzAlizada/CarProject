using Car.Domain.Common;
using System.Linq.Expressions;

namespace Car.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null);
        int GetCount(Expression<Func<T, bool>> expression = null);
        IQueryable<T> Query();



        Task<double> GetPagedCountAsync(double take, Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetPagedListAsync(int take, int page, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, Expression<Func<T, object>> orderByDescending = null);
    }
}
