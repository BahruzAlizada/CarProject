using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PreferencesController : Controller
    {
        private readonly IPreferenceReadRepository preferenceReadRepository;
        private readonly IPreferenceWriteRepository preferenceWriteRepository;
        public PreferencesController(IPreferenceReadRepository preferenceReadRepository, IPreferenceWriteRepository preferenceWriteRepository)
        {
            this.preferenceReadRepository = preferenceReadRepository;
            this.preferenceWriteRepository = preferenceWriteRepository;
        }
        #region Index
        public IActionResult Index()
        {
            List<Preference> preferences = preferenceReadRepository.GetAll();
            return View(preferences);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Preference preference)
        {
            #region IsExist
            bool result = preferenceReadRepository.GetAll().Any(x => x.Name == preference.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            preferenceWriteRepository.Add(preference);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            Preference preference = preferenceReadRepository.Get(x => x.Id == id);
            if (preference == null) return BadRequest();

            return View(preference);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid? id, Preference newPreference)
        {
            if (id == null) return NotFound();
            Preference preference = preferenceReadRepository.Get(x => x.Id == id);
            if (preference == null) return BadRequest();

            #region IsExist
            bool result = preferenceReadRepository.GetAll().Any(x => x.Name == newPreference.Name && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion


            preference.Name = newPreference.Name;

            preferenceWriteRepository.Update(preference);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Preference preference = preferenceReadRepository.Get(x => x.Id == id);
            if (preference == null) return BadRequest();

            preferenceWriteRepository.Activity(preference);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
