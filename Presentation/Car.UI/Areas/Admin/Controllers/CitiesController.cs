using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CitiesController : Controller
    {
        private readonly ICityReadRepository cityReadRepository;
        private readonly ICityWriteRepository cityWriteRepository;
        public CitiesController(ICityReadRepository cityReadRepository, ICityWriteRepository cityWriteRepository)
        {
            this.cityReadRepository = cityReadRepository;
            this.cityWriteRepository = cityWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<City> cities = cityReadRepository.GetAll();
            return View(cities);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(City city)
        {
            #region IsExist
            bool result = cityReadRepository.GetAll().Any(x => x.Name == city.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            cityWriteRepository.Add(city);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            City city = cityReadRepository.Get(x => x.Id == id);
            if (city == null) return BadRequest();

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, City newCity)
        {
            if (id == null) return NotFound();
            City city = cityReadRepository.Get(x => x.Id == id);
            if (city == null) return BadRequest();

            #region IsExist
            bool result = cityReadRepository.GetAll().Any(x => x.Name == newCity.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            city.Name = newCity.Name;

            cityWriteRepository.Update(city);
            return RedirectToAction("Index");
        
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            City city = cityReadRepository.Get(x => x.Id == id);
            if (city == null) return BadRequest();

            cityWriteRepository.Activity(city);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
