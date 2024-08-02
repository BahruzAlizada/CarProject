using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnersController : Controller
    {
        private readonly IOwnerReadRepository ownerReadRepository;
        private readonly IOwnerWriteRepository ownerWriteRepository;
        public OwnersController(IOwnerReadRepository ownerReadRepository,IOwnerWriteRepository ownerWriteRepository)
        {
            this.ownerReadRepository = ownerReadRepository;
            this.ownerWriteRepository = ownerWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Owner> owners = ownerReadRepository.GetAll();
            return View(owners);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Owner owner)
        {
            #region IsExist
            bool result = ownerReadRepository.GetAll().Any(x => x.Name == owner.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            ownerWriteRepository.Add(owner);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Owner owner = ownerReadRepository.Get(x => x.Id == id);
            if (owner == null) return BadRequest();

            return View(owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, Owner newOwner)
        {
            if (id == null) return NotFound();
            Owner owner = ownerReadRepository.Get(x => x.Id == id);
            if (owner == null) return BadRequest(); ;

            #region IsExist
            bool result = ownerReadRepository.GetAll().Any(x => x.Name == newOwner.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            owner.Name = newOwner.Name;

            ownerWriteRepository.Update(owner);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Owner owner = ownerReadRepository.Get(x => x.Id == id);
            if (owner == null) return BadRequest();

            ownerWriteRepository.Activity(owner);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
