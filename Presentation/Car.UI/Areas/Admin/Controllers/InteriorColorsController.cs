using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Infrastructure.Abstract;
using Car.Infrastructure.Concrete;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InteriorColorsController : Controller
    {
        private readonly IInteriorColorReadRepository interiorColorReadRepository;
        private readonly IInteriorColorWriteRepository interiorColorWriteRepository;
        private readonly IWebHostEnvironment env;
        private readonly IPhotoService photoService;
        public InteriorColorsController(IInteriorColorReadRepository interiorColorReadRepository, IInteriorColorWriteRepository interiorColorWriteRepository,
            IWebHostEnvironment env, IPhotoService photoService)
        {
            this.interiorColorReadRepository = interiorColorReadRepository;
            this.interiorColorWriteRepository = interiorColorWriteRepository;
            this.env = env;
            this.photoService = photoService;
        }

        #region Index
        public IActionResult Index()
        {
            List<InteriorColor> interiorColors = interiorColorReadRepository.GetAll();
            return View(interiorColors);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(InteriorColor interiorColor)
        {

            #region Image
            (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(interiorColor.Photo, false, true);
            if (!isValid)
            {
                ModelState.AddModelError("Photo", errorMessage);
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "admin", "assets", "img", "interiorcolor");
            interiorColor.Image = await photoService.SavePhotoAsync(interiorColor.Photo, folder);
            #endregion

            await interiorColorWriteRepository.AddAsync(interiorColor);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            InteriorColor interiorColor = interiorColorReadRepository.Get(x => x.Id == id);
            if (interiorColor == null) return BadRequest();

            return View(interiorColor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(Guid? id, InteriorColor newInteriorColor)
        {
            if (id == null) return NotFound();
            InteriorColor interiorColor = interiorColorReadRepository.Get(x => x.Id == id);
            if (interiorColor == null) return BadRequest();


            #region Image
            if (newInteriorColor.Photo is not null)
            {
                (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(newInteriorColor.Photo, true, true);
                if (!isValid)
                {
                    ModelState.AddModelError("Photo", errorMessage);
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "admin", "assets", "img", "interiorcolor");
                newInteriorColor.Image = await photoService.SavePhotoAsync(newInteriorColor.Photo, folder);

                string path = Path.Combine(env.WebRootPath, folder, interiorColor.Image);
                photoService.DeletePhoto(path);

                interiorColor.Image = newInteriorColor.Image;
            }
            #endregion

            interiorColor.Name = newInteriorColor.Name;

            await interiorColorWriteRepository.UpdateAsync(interiorColor);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            InteriorColor interiorColor = interiorColorReadRepository.Get(x => x.Id == id);
            if (interiorColor == null) return BadRequest();

            interiorColorWriteRepository.Activity(interiorColor);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
