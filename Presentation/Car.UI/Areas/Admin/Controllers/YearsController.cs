using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.Concrete;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YearsController : Controller
    {
        private readonly IYearReadRepository yearReadRepository;
        private readonly IYearWriteRepository yearWriteRepository;
        public YearsController(IYearReadRepository yearReadRepository, IYearWriteRepository yearWriteRepository)
        {
            this.yearReadRepository = yearReadRepository;
            this.yearWriteRepository = yearWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Year> years = yearReadRepository.GetAll();
            return View(years);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Year year)
        {
            #region IsExist
            bool result = yearReadRepository.GetAll().Any(x => x.Yearr == year.Yearr);
            if (result)
            {
                ModelState.AddModelError("Yearr", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            yearWriteRepository.Add(year);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Year year = yearReadRepository.Get(x => x.Id == id);
            if (year == null) return BadRequest();

            return View(year);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, Year newYear)
        {
            if (id == null) return NotFound();
            Year year = yearReadRepository.Get(x => x.Id == id);
            if (year == null) return BadRequest();

            #region IsExist
            bool result = yearReadRepository.GetAll().Any(x => x.Yearr == newYear.Yearr && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            year.Yearr = newYear.Yearr;
            
            yearWriteRepository.Update(year);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Year year = yearReadRepository.Get(x => x.Id == id);
            if (year == null) return BadRequest();

            yearWriteRepository.Activity(year);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
