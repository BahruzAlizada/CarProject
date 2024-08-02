using Car.Application.Abstract;
using Car.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BansController : Controller
    {
        private readonly IBanReadRepository banReadRepository;
        private readonly IBanWriteRepository banWriteRepository;
        public BansController(IBanReadRepository banReadRepository, IBanWriteRepository banWriteRepository)
        {
            this.banReadRepository = banReadRepository;
            this.banWriteRepository = banWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Ban> bans = banReadRepository.GetAll();
            return View(bans);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Ban ban)
        {
            #region IsExist
            bool result = banReadRepository.GetAll().Any(x => x.Name == ban.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            banWriteRepository.Add(ban);
            return RedirectToAction("Index"); 
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Ban ban = banReadRepository.Get(x => x.Id == id);
            if (ban == null) return BadRequest();

            return View(ban);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id,Ban newBan)
        {
            if (id == null) return NotFound();
            Ban ban = banReadRepository.Get(x=>x.Id==id);
            if (ban == null) return BadRequest();

            #region IsExist
            bool result = banReadRepository.GetAll().Any(x => x.Name == newBan.Name && x.Id!=id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            ban.Name = newBan.Name;

            banWriteRepository.Update(ban);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Ban ban = banReadRepository.Get(x => x.Id == id);
            if (ban == null) return BadRequest();

            banWriteRepository.Activity(ban);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
