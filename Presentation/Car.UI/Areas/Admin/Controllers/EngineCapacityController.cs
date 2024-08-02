using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EngineCapacityController : Controller
    {
        private readonly IEngineCapacityReadRepository engineCapacityReadRepository;
        private readonly IEngineCapacityWriteRepository engineCapacityWriteRepository;
        public EngineCapacityController(IEngineCapacityReadRepository engineCapacityReadRepository, IEngineCapacityWriteRepository engineCapacityWriteRepository)
        {
            this.engineCapacityReadRepository = engineCapacityReadRepository;
            this.engineCapacityWriteRepository = engineCapacityWriteRepository;
        }
        #region Index
        public IActionResult Index()
        {
            List<EngineCapacity> engineCapacities = engineCapacityReadRepository.GetAll();
            return View(engineCapacities);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EngineCapacity engineCapacity)
        {
            #region IsExist
            bool result = engineCapacityReadRepository.GetAll().Any(x => x.Name == engineCapacity.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            engineCapacityWriteRepository.Add(engineCapacity);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            EngineCapacity engineCapacity = engineCapacityReadRepository.Get(x => x.Id == id);
            if (engineCapacity == null) return BadRequest();

            return View(engineCapacity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, EngineCapacity newEngineCapacity)
        {
            if (id == null) return NotFound();
            EngineCapacity engineCapacity = engineCapacityReadRepository.Get(x => x.Id == id);
            if (engineCapacity == null) return BadRequest();

            #region IsExist
            bool result = engineCapacityReadRepository.GetAll().Any(x => x.Name == newEngineCapacity.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            engineCapacity.Name = newEngineCapacity.Name;

            engineCapacityWriteRepository.Update(engineCapacity);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            EngineCapacity engineCapacity = engineCapacityReadRepository.Get(x => x.Id == id);
            if (engineCapacity == null) return BadRequest();

            engineCapacityWriteRepository.Activity(engineCapacity);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
