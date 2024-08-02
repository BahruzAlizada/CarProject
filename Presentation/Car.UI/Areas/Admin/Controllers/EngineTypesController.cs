using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EngineTypesController : Controller
    {
        private readonly IEngineTypeReadRepository engineTypeReadRepository;
        private readonly IEngineTypeWriteRepository engineTypeWriteRepository;
        public EngineTypesController(IEngineTypeReadRepository engineTypeReadRepository, IEngineTypeWriteRepository engineTypeWriteRepository)
        {
            this. engineTypeReadRepository = engineTypeReadRepository;
            this.engineTypeWriteRepository = engineTypeWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<EngineType> engineTypes = engineTypeReadRepository.GetAll();
            return View(engineTypes);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EngineType engineType)
        {
            #region IsExist
            bool result = engineTypeReadRepository.GetAll().Any(x => x.Name == engineType.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            engineTypeWriteRepository.Add(engineType);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            EngineType engineType = engineTypeReadRepository.Get(x => x.Id == id);
            if (engineType == null) return BadRequest();

            return View(engineType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, EngineType newEngineType)
        {
            if (id == null) return NotFound();
            EngineType engineType = engineTypeReadRepository.Get(x => x.Id == id);
            if (engineType == null) return BadRequest();

            #region IsExist
            bool result = engineTypeReadRepository.GetAll().Any(x => x.Name == newEngineType.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            engineType.Name = newEngineType.Name;

            engineTypeWriteRepository.Update(engineType);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            EngineType engineType = engineTypeReadRepository.Get(x => x.Id == id);
            if (engineType == null) return BadRequest();

            engineTypeWriteRepository.Activity(engineType);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
