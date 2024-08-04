using AutoMapper;
using Car.Application.Abstract;
using Car.Application.Abstract.Model;
using Car.Application.ViewModels;
using Car.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelsController : Controller
    {
        private readonly IModelReadRepository modelReadRepository;
        private readonly IModelWriteRepository modelWriteRepository;
        private readonly IMapper mapper;
        private readonly IMarkaReadRepository markaReadRepository;
        public ModelsController(IModelReadRepository modelReadRepository, IModelWriteRepository modelWriteRepository,
            IMapper mapper, IMarkaReadRepository markaReadRepository)
        {
            this.modelReadRepository = modelReadRepository;
            this.modelWriteRepository = modelWriteRepository;
            this.markaReadRepository = markaReadRepository;
            this.mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index(string name, Guid? parentId, int page=1)
        {
            ViewBag.PageCount = await modelReadRepository.GetModelsPageCountAsync(25);
            ViewBag.CurrentPage = page;

            List<ModelListVM> modelListVMs = await modelReadRepository.GetModelsWithPageAsync(name, parentId, 25, page);
            return View(modelListVMs);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Markas = await markaReadRepository.GetAllAsync(x => x.IsMain && x.Status);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ModelVM model, Guid parentId)
        {
            ViewBag.Markas = await markaReadRepository.GetAllAsync(x => x.IsMain && x.Status);

            #region NameExisted
            bool result = modelReadRepository.GetAll().Any(x => x.Name == model.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            model.ParentId = parentId;

            Category category = mapper.Map<Category>(model);
            category.IsMain = false;

            await modelWriteRepository.AddAsync(category);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null) return NotFound();
            Category category = await modelReadRepository.GetAsync(x => x.Id == id);
            if (category == null) return BadRequest();

            ViewBag.Markas = await markaReadRepository.GetAllAsync(x => x.IsMain && x.Status);

            ModelVM model = new ModelVM
            {
                Name = category.Name,
                ParentId = (Guid)category.ParentId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(Guid? id, ModelVM newModel, Guid parentId)
        {
            if (id == null) return NotFound();
            Category category = await modelReadRepository.GetAsync(x => x.Id == id);
            if (category == null) return BadRequest();

            ViewBag.Markas = await markaReadRepository.GetAllAsync(x => x.IsMain && x.Status);

            ModelVM model = new ModelVM
            {
                Name = category.Name,
                ParentId = (Guid)category.ParentId
            };


            #region NameExisted
            bool result = modelReadRepository.GetAll().Any(x => x.Name == newModel.Name && x.Id!=id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            model.Name = newModel.Name;
            model.ParentId = parentId;

            category.Name = model.Name;
            category.ParentId = model.ParentId;

            await modelWriteRepository.UpdateAsync(category);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Category model = modelReadRepository.Get(x=>x.Id == id);
            if (model == null) return BadRequest();

            modelWriteRepository.Activity(model);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
