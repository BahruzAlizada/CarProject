using Car.Application.Abstract;
using Car.Application.ViewModels;
using Car.Domain.Entities;
using Car.Persistence.Concrete;
using Car.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Car.Persistence.EntityFramework
{
    public class MarkaReadRepository : ReadRepository<Category>, IMarkaReadRepository
    {
        public async Task<double> GetMarkasPageCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Categories.Where(x => x.IsMain).CountAsync() / take);
            return pageCount;
        }

        public async Task<List<MarkaListVM>> GetMarkasWithPageAsync(string search, int take, int page)
        {
            using var context = new Context();

            IQueryable<Category> categories = context.Categories.Where(x => x.IsMain).OrderBy(x => x.Name).AsQueryable();

            if (search is not null)
                categories = categories.Where(x => x.Name.Contains(search));

            List<Category> categoriesList = await categories.Skip((page-1)*take).Take(take).ToListAsync();
            List<MarkaListVM> markaVMs = new List<MarkaListVM>();

            foreach (var item in categoriesList)
            {
                MarkaListVM vm = new MarkaListVM
                {
                    Id = item.Id,
                    Image = item.Image,
                    Name = item.Name,
                    Status = item.Status,
                    ModelCount = await context.Categories.Where(x => x.ParentId == item.Id).CountAsync(),
                    CarCount = await context.Cars.Where(x => x.CategoryCars.Any(x => x.CategoryId == item.Id)).CountAsync(),
                };
                markaVMs.Add(vm);
            }
            return markaVMs;
        }
    }
}
