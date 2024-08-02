using Car.Application.Repositories;
using Car.Domain.Common;
using Car.Persistence.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Car.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    public T Get(Expression<Func<T, bool>> filter = null)
    {
        using var context = new Context();
        return filter == null
            ? context.Set<T>().FirstOrDefault()
            : context.Set<T>().FirstOrDefault(filter);

    }

    public List<T> GetAll(Expression<Func<T, bool>> filter = null)
    {
        using var context = new Context();
        return filter == null
            ? context.Set<T>().ToList()
            : context.Set<T>().Where(filter).ToList();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
    {
        using var context = new Context();
        return filter == null
            ? await context.Set<T>().ToListAsync()
            : await context.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
    {
        using var context = new Context();
        return filter == null
            ? await context.Set<T>().FirstOrDefaultAsync()
            : await context.Set<T>().FirstOrDefaultAsync(filter);
    }

    public int GetCount(Expression<Func<T, bool>> expression = null)
    {
        using var context = new Context();
        return expression == null ? context.Set<T>().Count() : context.Set<T>().Count(expression);
    }

    public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null)
    {
        using var context = new Context();
        return await context.Set<T>().CountAsync(expression);
    }

    public async Task<double> GetPagedCountAsync(double take, Expression<Func<T, bool>> filter = null)
    {
        using var context = new Context();

        var query = context.Set<T>().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        double totalCount = await query.CountAsync();
        double pageCount = Math.Ceiling(totalCount / take);
        return pageCount;
    }

    public async Task<List<T>> GetPagedListAsync(int take, int page, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, Expression<Func<T, object>> orderByDescending = null)
    {
        using var context = new Context();
        var query = context.Set<T>().Skip((page - 1) * take).Take(take);

        if (filter != null)
            query = query.Where(filter);
        if (orderBy != null)
            query = query.OrderBy(orderBy);
        if (orderByDescending != null)
            query = query.OrderByDescending(orderByDescending);

        return await query.ToListAsync();
    }

    public IQueryable<T> Query()
    {
        using var context = new Context();
        return context.Set<T>();
    }
}
