using AutoMapper;
using Car.Application.Abstract;
using Car.Application.ViewModels;
using Car.Domain.Entities;
using Car.Infrastructure.Abstract;
using Car.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Car.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarkasController : Controller
    {
        private readonly IMarkaReadRepository markaReadRepository;
        private readonly IMarkaWriteRepository markaWriteRepository;
        private readonly IWebHostEnvironment env;
        private readonly IPhotoService photoService;
        private readonly IMapper mapper;
        public MarkasController(IMarkaReadRepository markaReadRepository, IMarkaWriteRepository markaWriteRepository,
            IWebHostEnvironment env, IPhotoService photoService, IMapper mapper)
        {
            this.markaReadRepository = markaReadRepository;
            this.markaWriteRepository = markaWriteRepository;
            this.env = env;
            this.photoService = photoService;
            this.mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index(string name,int page = 1)
        {
            ViewBag.PageCount = await markaReadRepository.GetPagedCountAsync(20);
            ViewBag.CurrentPage = page;

            List<MarkaListVM> markaLists = await markaReadRepository.GetMarkasWithPageAsync(name, 20, page);
            return View(markaLists);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(MarkaVM marka)
        {
            #region NameExisted
            bool result = markaReadRepository.GetAll().Any(x => x.Name == marka.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            #region Image
            (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(marka.Photo, false, true);
            if (!isValid)
            {
                ModelState.AddModelError("Photo", errorMessage);
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "admin", "assets", "img", "marka");
            marka.Image = await photoService.SavePhotoAsync(marka.Photo, folder);
            #endregion

            Category category = mapper.Map<Category>(marka);
            category.IsMain = true;

            await markaWriteRepository.AddAsync(category);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null) return NotFound();
            Category category = await markaReadRepository.GetAsync(x => x.Id == id);
            if (category == null) return BadRequest();

            MarkaVM marka = new MarkaVM
            {
                Image = category.Image,
                Name = category.Name
            };

            return View(marka);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(Guid? id,MarkaVM newMarka)
        {
            if (id == null) return NotFound();
            Category category = await markaReadRepository.GetAsync(x => x.Id == id);
            if (category == null) return BadRequest();

            MarkaVM marka = new MarkaVM
            {
                Image = category.Image,
                Name = category.Name
            };

            #region NameExisted
            bool result = markaReadRepository.GetAll().Any(x => x.Name == marka.Name && x.Id!=id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda obyekt hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            #region Image
            if (newMarka.Photo is not null)
            {
                (bool isValid, string errorMessage) = await photoService.PhotoChechkValidatorAsync(newMarka.Photo, true, true);
                if (!isValid)
                {
                    ModelState.AddModelError("Photo", errorMessage);
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "admin", "assets", "img", "marka");
                newMarka.Image = await photoService.SavePhotoAsync(newMarka.Photo, folder);

                string path = Path.Combine(env.WebRootPath, folder, marka.Image);
                photoService.DeletePhoto(path);

                marka.Image = newMarka.Image;
            }
            #endregion

            marka.Name = newMarka.Name;

            category.Name = marka.Name;
            category.Image = marka.Image;

            await markaWriteRepository.UpdateAsync(category);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(Guid? id)
        {
            if (id == null) return NotFound();
            Category marka = markaReadRepository.Get(x => x.Id == id);
            if (marka == null) return BadRequest();

            markaWriteRepository.Activity(marka);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
