using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Infrastructure.Abstract;
using Car.Infrastructure.Concrete;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExteriorColorsController : Controller
    {
        private readonly IExteriorColorReadRepository exteriorColorReadRepository;
        private readonly IExteriorColorWriteRepository exteriorColorWriteRepository;
        private readonly IWebHostEnvironment env;
        private readonly IPhotoService photoService;
        public ExteriorColorsController(IExteriorColorReadRepository exteriorColorReadRepository, IExteriorColorWriteRepository exteriorColorWriteRepository,
            IWebHostEnvironment env, IPhotoService photoService)
        {
            this.exteriorColorReadRepository = exteriorColorReadRepository;
            this.exteriorColorWriteRepository = exteriorColorWriteRepository;
            this.env = env;
            this.photoService = photoService;
        }

        #region Index
        public IActionResult Index()
        {
            List<ExteriorColor> exteriorColors = exteriorColorReadRepository.GetAll();
            return View(exteriorColors);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ExteriorColor exteriorColor)
        {

            #region Image
            (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(exteriorColor.Photo, false, true);
            if (!isValid)
            {
                ModelState.AddModelError("Photo", errorMessage);
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "admin","assets","img", "exteriorcolor");
            exteriorColor.Image = await photoService.SavePhotoAsync(exteriorColor.Photo, folder);
            #endregion

            await exteriorColorWriteRepository.AddAsync(exteriorColor);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(Guid? id)
        {
            if (id == null) return NotFound();
            ExteriorColor exteriorColor = exteriorColorReadRepository.Get(x => x.Id == id);
            if (exteriorColor == null) return BadRequest();

            return View(exteriorColor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(Guid? id, ExteriorColor newExteriorColor)
        {
            if (id == null) return NotFound();
            ExteriorColor exteriorColor = exteriorColorReadRepository.Get(x => x.Id == id);
            if (exteriorColor == null) return BadRequest();


            #region Image
            if(newExteriorColor.Photo is not null)
            {
                (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(newExteriorColor.Photo, true, true);
                if (!isValid)
                {
                    ModelState.AddModelError("Photo", errorMessage);
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "admin", "assets", "img", "exteriorcolor");
                newExteriorColor.Image = await photoService.SavePhotoAsync(newExteriorColor.Photo, folder);

                string path = Path.Combine(env.WebRootPath, folder, exteriorColor.Image);
                photoService.DeletePhoto(path);

                exteriorColor.Image = newExteriorColor.Image;
            }
            #endregion

            exteriorColor.Name = newExteriorColor.Name;

            await exteriorColorWriteRepository.UpdateAsync(exteriorColor);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            ExteriorColor exteriorColor = exteriorColorReadRepository.Get(x => x.Id == id);
            if (exteriorColor == null) return BadRequest();

            exteriorColorWriteRepository.Activity(exteriorColor);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
