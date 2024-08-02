using Car.Application.Abstract;
using Car.Domain.Entities;
using Car.Infrastructure.Abstract;
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
        public MarkasController(IMarkaReadRepository markaReadRepository, IMarkaWriteRepository markaWriteRepository,
            IWebHostEnvironment env, IPhotoService photoService)
        {
            this.markaReadRepository = markaReadRepository;
            this.markaWriteRepository = markaWriteRepository;
            this.env = env;
            this.photoService = photoService;
        }

        #region Index
        public IActionResult Index()
        {
            List<Category> categories = markaReadRepository.GetAll();
            return View(categories);
        }
        #endregion
    }
}
