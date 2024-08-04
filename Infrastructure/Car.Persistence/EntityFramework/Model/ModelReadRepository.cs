using Car.Application.Abstract;
using Car.Application.ViewModels;
using Car.Domain.Entities;
using Car.Persistence.Concrete;
using Car.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Car.Persistence.EntityFramework
{
    public class ModelReadRepository : ReadRepository<Category>, IModelReadRepository
    {
        public async Task<double> GetModelsPageCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Categories.Where(x => !x.IsMain).CountAsync() / take);
            return pageCount;
        }

        public async Task<List<ModelListVM>> GetModelsWithPageAsync(string name, Guid? parentId, int take, int page)
        {
            using var context = new Context();

            IQueryable<Category> models = context.Categories.Where(x => !x.IsMain).Include(x=>x.Parent).OrderBy(x => x.Name).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                models = models.Where(x => x.Name.Contains(name));

            if (parentId != null)
                models = models.Where(x => x.ParentId == parentId);

            List<Category> modelList = await models.Skip((page - 1) * take).ToListAsync();
            List<ModelListVM> modelListVMs = new List<ModelListVM>();

            foreach (var item in modelList)
            {
                ModelListVM vm = new ModelListVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    ParentName = item.Parent.Name,
                    Status = item.Status,
                    CarCount = await context.Cars.Where(x => x.CategoryCars.Any(xp => xp.CategoryId == item.Id)).CountAsync()
                };
                modelListVMs.Add(vm);
            }
            return modelListVMs;
        }
    }
}
