using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GearBoxesController : Controller
    {
        private readonly IGearBoxReadRepository gearBoxReadRepository;
        private readonly IGearBoxWriteRepository gearBoxWriteRepository;
        public GearBoxesController(IGearBoxReadRepository gearBoxReadRepository,IGearBoxWriteRepository gearBoxWriteRepository)
        {
            this.gearBoxReadRepository = gearBoxReadRepository;
            this.gearBoxWriteRepository = gearBoxWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<GearBox> gearBoxes = gearBoxReadRepository.GetAll();
            return View(gearBoxes);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(GearBox gearBox)
        {
            #region IsExist
            bool result = gearBoxReadRepository.GetAll().Any(x => x.Name == gearBox.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            gearBoxWriteRepository.Add(gearBox);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            GearBox gearBox = gearBoxReadRepository.Get(x => x.Id == id);
            if (gearBox == null) return BadRequest();

            return View(gearBox);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, GearBox newGearBox)
        {
            if (id == null) return NotFound();
            GearBox gearBox = gearBoxReadRepository.Get(x => x.Id == id);
            if (gearBox == null) return BadRequest();

            #region IsExist
            bool result = gearBoxReadRepository.GetAll().Any(x => x.Name == newGearBox.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            gearBox.Name = newGearBox.Name;

            gearBoxWriteRepository.Update(gearBox);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            GearBox gearBox = gearBoxReadRepository.Get(x => x.Id == id);
            if (gearBox == null) return BadRequest();

            gearBoxWriteRepository.Activity(gearBox);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
